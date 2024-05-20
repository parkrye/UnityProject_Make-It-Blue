using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActor : BaseActor
{
    public PlayerActorController Controller { get; private set; }

    public UnityEvent<float> HPRatioEvent = new UnityEvent<float>();
    public UnityEvent<float> SPRatioEvent = new UnityEvent<float>();

    public bool IsControllable { get; set; }
    public int SelectIndex { get; set; }
    public float MoveSpeed { get; private set; }
    public float TurnSpeed { get; private set; }

    private Playable[] _playables;

    private void Awake()
    {
        Controller = GetComponent<PlayerActorController>();
        if (Controller == null)
            Debug.Log(gameObject.name + " lost PlayerActorController");

        _playables = GetComponentsInChildren<Playable>().OrderBy(t => t.name).ToArray();
        if (_playables == null)
            Debug.Log(gameObject.name + " lost Playables");

        SelectIndex = 0;
        IsControllable = false;
    }

    public override void Init()
    {
        base.Init();

        _nowHP = StaticValues.DefaultHP;
        _nowSP = StaticValues.DefaultSP;
        MoveSpeed = StaticValues.DefaultMoveSpeed;
        TurnSpeed = StaticValues.DefaultTurnSpeed;

        _type = ActorType.PC;
        _state = ActorState.Alive;

        for (int i = 0;i < _playables.Length; i++)
        {
            _playables[i].gameObject.SetActive(i == SelectIndex);
        }

        IsControllable = true;
    }

    public void InputControllVector(Vector2 input, bool isForMove)
    {
        if (isForMove)
            Controller.Move(input);
        else
            Controller.Turn(input);
    }
}
