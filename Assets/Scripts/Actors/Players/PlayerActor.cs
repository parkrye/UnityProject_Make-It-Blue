using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class PlayerActor : BaseActor, IConditionalbe
{
    public PlayerActorController Controller { get; private set; }
    public PlayerCameraController Camera { get; private set; }

    public bool IsControllable { get; set; }
    public int SelectIndex { get; set; }
    public float MoveSpeed { get; private set; }
    public float TurnSpeed { get; private set; }

    private ActorAnimationController[] _actorAnimationControllers;
    private ActorAnimationController AnimController { get { return _actorAnimationControllers[SelectIndex]; } }
    private bool _isBattle, _isLoopAction;

    public WeaponData WeaponData { get; private set; }
    private BaseAction MainAction;
    private List<BaseAction> SubActions = new List<BaseAction>();
    private int _subActionIndex;

    public Transform Model;
    public Transform Focus;
    public Vector3 CenterPosition { get { return transform.position + Vector3.up; } }

    public UnityEvent<float> HPRatioEvent = new UnityEvent<float>();
    public UnityEvent<float> SPRatioEvent = new UnityEvent<float>();

    private Dictionary<ConditionEnum, bool> _conditions = new Dictionary<ConditionEnum, bool>();

    private void Awake()
    {
        Controller = GetComponent<PlayerActorController>();
        if (Controller == null)
            Debug.Log(gameObject.name + " lost PlayerActorController");

        Camera = GetComponent<PlayerCameraController>();
        if (Camera == null)
            Debug.Log(gameObject.name + " lost PlayerCameraController");

        if (Focus == null)
            Debug.Log(gameObject.name + " lost Focus");

        var etc = GetComponentInChildren<ExternalTriggerChecker>();
        if (etc != null)
        {
            etc.TriggerEnter.AddListener(TriggerEnter);
            etc.TriggerStay.AddListener(TriggerStay);
            etc.TriggerExit.AddListener(TriggerExit);
        }

        SelectIndex = 0;
        IsControllable = false;
    }

    private void InitDefault()
    {
        _nowHP = StaticValues.DefaultHP;
        _nowSP = StaticValues.DefaultSP;
        MoveSpeed = GameManager.Data.Play.GetStatus(StatusEnum.MoveSpeed);
        TurnSpeed = StaticValues.DefaultTurnSpeed;

        _type = ActorType.PC;
        _state = ActorState.Alive;


        _actorAnimationControllers = GetComponentsInChildren<ActorAnimationController>().OrderBy(t => t.name).ToArray();
        if (_actorAnimationControllers == null)
            Debug.Log(gameObject.name + " lost ActorAnimationController");

        for (int i = 0; i < _actorAnimationControllers.Length; i++)
        {
            _actorAnimationControllers[i].gameObject.SetActive(i == SelectIndex);
        }

        IsControllable = true;
    }

    public override void InitForWorld()
    {
        base.InitForWorld();
        InitDefault();

        _isBattle = false;
        if (_actorAnimationControllers.Length > 0)
            AnimController.ToggleBattleValue(_isBattle);

        MainAction = new Action_Interaction(ActionEnum.None);
        SubActions.Add(new Action_Emotions(ActionEnum.OnAction5));
        SubActions.Add(new Action_Emotions(ActionEnum.OnAction6));
        SubActions.Add(new Action_Emotions(ActionEnum.OnAction7));
        SubActions.Add(new Action_Emotions(ActionEnum.OnAction8));
        SubActions.Add(new Action_Emotions(ActionEnum.OnAction9));
        SubActions.Add(new Action_Emotions(ActionEnum.Dance));
    }

    public override void InitForBattle()
    {
        base.InitForBattle();
        InitDefault();

        _isBattle = true;
        if (_actorAnimationControllers.Length > 0)
            AnimController.ToggleBattleValue(_isBattle);
    }

    public void InputControllVector(Vector2 input, bool isForMove)
    {
        if (isForMove)
        {
            Controller.Move(input);
            if (_actorAnimationControllers.Length > 0)
                AnimController.PlayMove(input);
        }
        else
        {
            Controller.Turn(input);
            if (_actorAnimationControllers.Length > 0)
                AnimController.PlayTurn(input.x);
        }
    }

    public void OnMainAction()
    {
        Controller.Action(MainAction);
        if (_actorAnimationControllers.Length > 0)
            AnimController.PlayAction(MainAction);
    }

    public void OnLoopActionStart()
    {
        _isLoopAction = true;

        if (_isBattle)
            LoopActionTask().Forget();
    }

    public void OnLoopActionEnd()
    {
        _isLoopAction = false;
        if (_actorAnimationControllers.Length > 0)
            AnimController.ToggleLoopValue(false);
    }

    private async UniTask LoopActionTask()
    {
        var timer = 0f;
        while (timer < 0.5f && _isLoopAction)
        {
            await UniTask.Delay(100);
            timer += 0.1f;
        }

        if (_isLoopAction)
        {
            if (_actorAnimationControllers.Length > 0)
            {
                AnimController.ToggleLoopValue(transform);
                AnimController.PlayAction(MainAction);
            }
        }

        while (_isLoopAction)
        {
            await UniTask.Delay(100);
            Controller.Action(MainAction);
        }

        if (_isLoopAction == false)
        {
            if (_actorAnimationControllers.Length > 0)
                AnimController.ToggleLoopValue(false);
        }
    }

    public void OnSubAction()
    {
        Controller.Action(SubActions[_subActionIndex]);
        if (_actorAnimationControllers.Length > 0)
            AnimController.PlayAction(SubActions[_subActionIndex]);
    }

    public void OnDragSubAction(DirectionEnum _, DirectionEnum lr)
    {
        switch (lr)
        {
            default:
            case DirectionEnum.Left:
                _subActionIndex--;
                if (_subActionIndex < 0)
                    _subActionIndex = SubActions.Count - 1;
                break;
            case DirectionEnum.Right:
                _subActionIndex++;
                if (_subActionIndex >= SubActions.Count)
                    _subActionIndex = 0;
                break;
        }
    }

    private void TriggerEnter(Collider other)
    {

    }


    private void TriggerStay(Collider other)
    {

    }


    private void TriggerExit(Collider other)
    {

    }

    public void EquipEquipments(WeaponData equipmentData = null, List<ItemData> items = null)
    {
        if (equipmentData != null)
        {
            switch (equipmentData.Type)
            {
                case ProductEnum.Weapon:
                case ProductEnum.Weapon_HG:
                case ProductEnum.Weapon_AR:
                case ProductEnum.Weapon_SG:
                case ProductEnum.Weapon_MG:
                    equipmentData.Weapon.Init();
                    if (equipmentData.Weapon.MainAction != null)
                        MainAction = equipmentData.Weapon.MainAction;
                    if (equipmentData.Weapon.SubAction != null)
                        SubActions.Add(equipmentData.Weapon.SubAction);
                    WeaponData = equipmentData;
                    break;
            }

            if (_actorAnimationControllers.Length > 0)
                AnimController.EquipWeapon(equipmentData);
        }

        if (items != null)
        {
            foreach (var item in items)
            {
                switch (item.Type)
                {
                    case ProductEnum.Item_BulletHG:
                    case ProductEnum.Item_BulletAR:
                    case ProductEnum.Item_BulletSG:
                    case ProductEnum.Item_BulletMG:
                        item.Item.Init();
                        if (item.Item.SubAction != null)
                            SubActions.Add(item.Item.SubAction);
                        break;
                    case ProductEnum.Item_Shield:
                        item.Item.Init();
                        break;
                }
            }
        }
    }

    public override void Hit(int damage)
    {
        _nowHP -= damage;
        if (_nowHP <= 0)
        {
            ActorDiedEvent?.Invoke(true);
        }
    }

    public override float GetStatus(StatusEnum status)
    {
        return GameManager.Data.Play.GetStatus(status);
    }

    public int GetCondition()
    {
        return _conditions.Count;
    }

    public void OccurCondition(ConditionEnum condition)
    {
        if (_conditions.ContainsKey(condition))
            return;
        _conditions[condition] = true;
    }

    public void RemoveCondition(ConditionEnum condition)
    {
        if (_conditions.ContainsKey(condition) == false)
            return;
        _conditions.Remove(condition);
    }

    ConditionEnum IConditionalbe.GetCondition()
    {
        var result = ConditionEnum.None;
        foreach (var (condition, value) in _conditions)
        {
            if (value)
                result &= condition;
        }
        return result;
    }

    public int GetConditionCount()
    {
        var result = 0;
        foreach (var (_, value) in _conditions)
        {
            if (value)
                result++;
        }
        return result;
    }
}
