using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    public Vector3 Velocity => _agent.velocity;
    public bool HasPath => _agent.hasPath;
    private ITargetProvider TargetProvider;

    private void Awake()
    {
        TargetProvider = GetComponent<ITargetProvider>();
    }

    private void Update()
    {
        if (TargetProvider != null) SetDestination();
    }

    public void SetDestination()
    {
        var target = TargetProvider.GetTarget();
        _agent?.SetDestination(target.position);
    }

    public void Stop()
    {
        SetDestination();
    }
}
