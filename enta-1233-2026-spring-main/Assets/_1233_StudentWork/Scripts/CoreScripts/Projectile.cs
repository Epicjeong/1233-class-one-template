using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private bool _useGravity;

    private Rigidbody _rb;
    private GameObject _source;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = _useGravity;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _source) return;

        var damageReciever = collision.gameObject.GetComponent<IDamageReciever>();
    }
}
