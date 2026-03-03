using UnityEngine;
using UnityEngine.AI;

//For enemies that want to get close to the player
public class ChasePlayer : MonoBehaviour, IMover
{
    [SerializeField] private NavMeshAgent _agent;

    public Vector3 Velocity => _agent.velocity;
    public bool HasPath => _agent.hasPath;
    private ITargetProvider TargetProvider;
    public float RemainingDistance => _agent.stoppingDistance;
    public bool IsAtDestination => _agent.isStopped;

    private void Awake()
    {
        TargetProvider = GetComponent<ITargetProvider>();
    }

    private void Update()
    {
        
    }

    public void SetDestination(Vector3 destination)
    {
        //var target = TargetProvider.GetTarget();
        _agent?.SetDestination(destination);
    }

    public void Stop()
    {
        _agent?.ResetPath();
    }

    public void Resume()
    {

    }

    public void Enabled(bool enabled)
    {

    }
}
