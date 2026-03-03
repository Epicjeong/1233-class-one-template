using System.Collections;
using UnityEditor.ShaderGraph.Configuration;
using UnityEngine;
using UnityEngine.AI;

public class SpikePatrol : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    public Transform[] _patrolPoints;
    public Vector3 Velocity => _agent.velocity;
    public bool HasPath => _agent.hasPath;
    private int _nextPoint = 0;
    private float _stoppingDistance;

    private void Awake()
    {
        SetDestination();
    }

    public void SetDestination()
    {
        var target = _patrolPoints[_nextPoint];
        _agent?.SetDestination(target.position);
    }

    private void Update()
    {
        if (_agent.remainingDistance <= _stoppingDistance)
            StartCoroutine(ChangePoints());
    }

    private IEnumerator ChangePoints()
    {
        _nextPoint++;
        if (_nextPoint > _patrolPoints.Length) _nextPoint = 0;
        yield return new WaitForSeconds(1f);
        SetDestination();
    }
}
