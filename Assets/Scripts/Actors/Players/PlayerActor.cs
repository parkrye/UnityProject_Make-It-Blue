using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActor : BaseActor
{
    public CharacterController Controller { get; private set; }

    public UnityEvent<float> HPRatioEvent = new UnityEvent<float>();
    public UnityEvent<float> SPRatioEvent = new UnityEvent<float>();

    public bool IsControllable { get; set; }

    private Playable[] _playables;
    private int _playableIndex;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        if (Controller == null)
            Debug.Log(gameObject.name + " lost CharacterController");

        _playables = GetComponentsInChildren<Playable>().OrderBy(t => t.name).ToArray();
        if (_playables == null)
            Debug.Log(gameObject.name + " lost Playables");

        _playableIndex = 0;
        IsControllable = false;
    }

    public override void Init()
    {
        base.Init();

        _type = ActorType.PC;
        _state = ActorState.Alive;

        for (int i = 0;i < _playables.Length; i++)
        {
            _playables[i].gameObject.SetActive(i == _playableIndex);
        }

        IsControllable = true;
    }
}
