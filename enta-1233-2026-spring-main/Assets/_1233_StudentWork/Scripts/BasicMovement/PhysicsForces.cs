using UnityEngine;

public class PhysicsForces : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Vector3 _force;
    [SerializeField] private bool _continuous;
    [SerializeField] private ForceMode _forceMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody.isKinematic = false;
        _rigidBody.useGravity = true;
        if (!_continuous)
            _rigidBody.AddForce(_force, _forceMode);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_continuous)
            _rigidBody.AddForce(_force, _forceMode);
    }
}
