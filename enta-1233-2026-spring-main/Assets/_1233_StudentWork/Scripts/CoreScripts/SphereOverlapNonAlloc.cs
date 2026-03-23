using UnityEngine;
using UnityEngine.Events;

public class SphereOverlapNonAlloc : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _maxTargets = 32;

    [SerializeField] private UnityEvent<Collider> _onColliderDetected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
