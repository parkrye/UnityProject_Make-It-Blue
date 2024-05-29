using UnityEngine;
using UnityEngine.AI;

public class EnemyActorController : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Vector3 _findPosition;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
            Debug.Log($"{name} lost NavMeshSurface!");
    }

    public void Tick()
    {
        _agent.SetDestination(_findPosition);
    }

    private void FindPlayer()
    {

    }

    private void Attack()
    {

    }
}
