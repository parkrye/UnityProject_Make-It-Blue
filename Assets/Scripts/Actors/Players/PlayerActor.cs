using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActor : BaseActor
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

    public EquipmentData WeaponData { get; private set; }
    private BaseAction MainAction;
    private List<BaseAction> SubActions = new List<BaseAction>();
    private int _subActionIndex;

    public Transform Model;
    public Transform Focus;
    public Vector3 CenterPosition { get { return transform.position + Vector3.up; } }

    public UnityEvent<float> HPRatioEvent = new UnityEvent<float>();
    public UnityEvent<float> SPRatioEvent = new UnityEvent<float>();
    public UnityEvent PlayerDiedEvent = new UnityEvent();

    private void Awake()
    {
        Controller = GetComponent<PlayerActorController>();
        if (Controller == null)
            Debug.Log(gameObject.name + " lost PlayerActorController");

        Camera = GetComponent<PlayerCameraController>();
        if (Camera == null)
            Debug.Log(gameObject.name + " lost PlayerCameraController");

        _actorAnimationControllers = GetComponentsInChildren<ActorAnimationController>().OrderBy(t => t.name).ToArray();
        if (_actorAnimationControllers == null)
            Debug.Log(gameObject.name + " lost ActorAnimationController");

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

        MainAction = null;
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
            AnimController.PlayAction(MainAction.ActionCode);
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
                AnimController.PlayAction(MainAction.ActionCode);
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
            AnimController.PlayAction(SubActions[_subActionIndex].ActionCode);
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

    public void EquipEquipment(EquipmentData equipmentData)
    {
        switch (equipmentData.Type)
        {
            case ProductEnum.Equipment:
            case ProductEnum.Equipment_HG:
            case ProductEnum.Equipment_AR:
            case ProductEnum.Equipment_SG:
            case ProductEnum.Equipment_MG:
                equipmentData.Equipment.Init();
                if (equipmentData.Equipment.MainAction != null)
                    MainAction = equipmentData.Equipment.MainAction;
                if (equipmentData.Equipment.SubAction != null)
                    SubActions.Add(equipmentData.Equipment.SubAction);
                WeaponData = equipmentData;
                break;
            case ProductEnum.Equipment_BulletHG:
            case ProductEnum.Equipment_BulletAR:
            case ProductEnum.Equipment_BulletSG:
            case ProductEnum.Equipment_BulletMG:
            case ProductEnum.Equipment_Other:
            case ProductEnum.Equipment_Shield:
                equipmentData.Equipment.Init();
                if (equipmentData.Equipment.SubAction != null)
                    SubActions.Add(equipmentData.Equipment.SubAction);
                break;
        }

        if (_actorAnimationControllers.Length > 0)
            AnimController.EquipWeapon(equipmentData);
    }
}
