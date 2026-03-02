using UnityEngine;
using UnityEngine.AI;

public class SetDestinationOnStart : MonoBehaviour
{

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _destination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _agent?.SetDestination(_destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
