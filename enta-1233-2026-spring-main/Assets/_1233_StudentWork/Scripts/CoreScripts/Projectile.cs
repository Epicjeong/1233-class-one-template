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
        if (damageReciever != null)
        {
            var info = new DamageInfo
            {
                Amount = _damage,
                Source = _source,
                HitPoint = collision.contacts[0].point,
                HitNormal = collision.contacts[0].normal
            };
            damageReciever.ApplyDamage(info);
        }
        Destroy(gameObject);
    }

    public void Launch(Vector3 direction, GameObject source)
    {
        _source = source;
        _rb.linearVelocity = direction.normalized * _speed;
        transform.forward = direction;
        Destroy(gameObject, _lifetime);
    }
    public void LaunchWithVelocity(Vector3 velocity, GameObject source)
    {
        _source = source;
        _rb.linearVelocity = velocity;
        if (velocity.sqrMagnitude > 0.0001f) transform.forward = velocity;
        _rb.useGravity = true;
        Destroy(gameObject, _lifetime);
    }

}
