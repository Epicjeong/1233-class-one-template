using UnityEngine;

public class SpikeBrain : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private PatrolMotor _patrolMotor;
    [SerializeField] private ContactDamage _contactDamage;
    [SerializeField] private EnemyAnimDriver _animDriver;

    private IMover _mover;

    private void Awake()
    {
        if (_health == null) GetComponent<Health>();
        if (_patrolMotor == null) GetComponent<PatrolMotor>();
        if (_contactDamage == null) GetComponent<ContactDamage>();
        if (_animDriver == null) GetComponent<EnemyAnimDriver>();
        _mover = GetComponent<IMover>();
    }

    private void Update()
    {
        if (_health != null && _health.IsDead) return;

        if (_animDriver != null && _mover != null) _animDriver.SetSpeed(_mover.Velocity.magnitude);
    }

    public void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDied += HandleDied;
        }
    }

    public void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDied -= HandleDied;
        }
    }

    private void HandleDied()
    {
        if (_patrolMotor != null) _patrolMotor.enabled = false;
        if (_contactDamage != null) _contactDamage.enabled = false;
        if (_mover != null)
        {
            _mover.Stop();
            _mover.Enabled(false);
        }
    }
}
