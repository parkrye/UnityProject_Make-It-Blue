using Cysharp.Threading.Tasks;
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

    private Playable[] _playables;
    private Playable Character { get { return _playables[SelectIndex]; } }
    private bool _isBattle, _isLoopAction;

    public UnityEvent<float> HPRatioEvent = new UnityEvent<float>();
    public UnityEvent<float> SPRatioEvent = new UnityEvent<float>();

    private void Awake()
    {
        Controller = GetComponent<PlayerActorController>();
        if (Controller == null)
            Debug.Log(gameObject.name + " lost PlayerActorController");

        Camera = GetComponent<PlayerCameraController>();
        if (Camera == null)
            Debug.Log(gameObject.name + " lost PlayerCameraController");

        _playables = GetComponentsInChildren<Playable>().OrderBy(t => t.name).ToArray();
        if (_playables == null)
            Debug.Log(gameObject.name + " lost Playables");

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
        MoveSpeed = StaticValues.DefaultMoveSpeed;
        TurnSpeed = StaticValues.DefaultTurnSpeed;

        _type = ActorType.PC;
        _state = ActorState.Alive;

        for (int i = 0; i < _playables.Length; i++)
        {
            _playables[i].gameObject.SetActive(i == SelectIndex);
        }

        IsControllable = true;
    }

    public override void InitForWorld()
    {
        base.InitForWorld();
        InitDefault();

        _isBattle = false;

        Character.SetGunAnimationValue((int)WeaponEnum.HG);
    }

    public override void InitForBattle()
    {
        base.InitForBattle();
        InitDefault();

        _isBattle = true;

        Character.SetGunAnimationValue((int)WeaponEnum.HG);
    }

    public void InputControllVector(Vector2 input, bool isForMove)
    {
        if (isForMove)
        {
            Controller.Move(input);
            Character.PlayMoveAnimation(input);
        }
        else
        {
            Controller.Turn(input);
        }
    }

    public void OnMainActionStart()
    {
        _isLoopAction = true;
        if (_isBattle)
        {
            Character.PlayActionAnimation(0);
            LoopActionTask().Forget();
        }
        else
        {

        }
    }

    public void OnMainActionEnd()
    {
        _isLoopAction = false;
        Character.ToggleLoopAnimation(false);
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
            Character.ToggleLoopAnimation(transform);
            Character.PlayActionAnimation(0);
        }
        while (_isLoopAction)
        {
            await UniTask.Delay(100);
        }
        if (_isLoopAction == false)
            Character.ToggleLoopAnimation(false);
    }

    public void OnSubAction()
    {
        if (_isBattle)
        {
            Character.ToggleEquipAnimation();
        }
        else
        {

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

    public void EquipWeapon(WeaponData weapon)
    {
        Character.EquipWeapon(weapon);
    }
}
