using UnityEngine;
using UnityEngine.Events;

public class BattleSpace : MonoBehaviour
{
    private SpaceDivider _spaceDivider;
    public Transform PlayerSpawnPosition;
    public Transform[] EnemySpawnPositions;

    public UnityEvent InitSpaceEndEvent = new UnityEvent();

    public void InitSpace()
    {
        _spaceDivider = GetComponent<SpaceDivider>();
        if (_spaceDivider == null)
            Debug.Log($"{name} lost SpaceDivider!");

        _spaceDivider.DivideEndEvent.AddListener(AfterSpaceDivideAction);
        _spaceDivider.DivdeSpace();
    }

    private void AfterSpaceDivideAction(Space rootSpace)
    {
        EnemySpawnPositions = GameObject.Find("EnemySpawnPosition").GetComponentsInChildren<Transform>();

        InitSpaceEndEvent?.Invoke();
    }
}
