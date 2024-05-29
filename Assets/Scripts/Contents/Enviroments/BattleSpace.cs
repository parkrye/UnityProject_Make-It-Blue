using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BattleSpace : MonoBehaviour
{
    public Transform PlayerSpawnPosition;
    public Transform[] EnemySpawnPositions;
    public UnityEvent InitSpaceEndEvent = new UnityEvent();

    private NavMeshSurface[] _navMeshSurfaces;

    public void InitSpace()
    {
        _navMeshSurfaces = GetComponentsInChildren<NavMeshSurface>();
        if (_navMeshSurfaces == null)
            Debug.Log($"{name} lost NavMeshSurface!");

        foreach (var navMeshSurface in _navMeshSurfaces)
        {
            navMeshSurface.RemoveData();
            navMeshSurface.BuildNavMesh();
        }

        EnemySpawnPositions = GameObject.Find("EnemySpawnPosition").GetComponentsInChildren<Transform>();

        InitSpaceEndEvent?.Invoke();
    }
}
