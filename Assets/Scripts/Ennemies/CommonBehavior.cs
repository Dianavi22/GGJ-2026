using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CommonBehavior : MonoBehaviour
{
    [SerializeField] protected Transform[] _behaviorPoints;
    [SerializeField] protected Transform _origin;
    [SerializeField] protected Transform _playerTarget;

    protected int _behaviorIndex = 0;

    protected NavMeshAgent _agent;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = 1;
        SetDestination(_playerTarget);

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        SetDestination(_playerTarget);
    }

    protected void SetDestination(Transform[] targets, int index)
    {
        _agent.destination = targets[index].position;
    }
    protected void SetDestination(Transform target)
    {
        _agent.destination = target.position;
    }

    protected bool IsDestinationComplete()
    {
        return !_agent.hasPath || (_agent.remainingDistance <= _agent.stoppingDistance);
    }
}
