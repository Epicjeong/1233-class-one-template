using UnityEngine;
using UnityEngine.Events;

public class SphereOverlapNonAlloc : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _maxHits = 32;

    [SerializeField] private UnityEvent<Collider> _onColliderDetected;

    private Collider[] _results;

    private void Awake()
    {
        _results = new Collider[_maxHits];
    }

    public void CheckOverlap()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _results, _layerMask);

        for (int i = 0; i < count; i++)
        {
            _onColliderDetected?.Invoke(_results[i]);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
