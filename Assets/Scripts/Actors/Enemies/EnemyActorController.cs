using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyActorController : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
            Debug.Log($"{name} lost NavMeshSurface!");
    }

    public void Tick()
    {
        _agent.SetDestination(GameManager.System.PlayerActor.transform.position);
    }
}
