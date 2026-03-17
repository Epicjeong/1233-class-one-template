using UnityEngine;

public class TrainingDummyBrain : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private EnemyAnimDriver _animDriver;
    [SerializeField] private float _resetDelay = 2f;
    [SerializeField] private bool _autoReset = true;

    public void Awake()
    {
        if (_health == null) GetComponent<Health>();
        if (_animDriver == null) GetComponent<EnemyAnimDriver>();
    }

    public void OnEnable()
    {
        if ( _health != null)
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnDied += HandleDied;
        }
    }

    public void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDied -= HandleDied;
        }
    }

    private void HandleDamaged(DamageInfo info)
    {
        if (_health != null)
        {
            Debug.Log($"Dummy hit by " +
            $"{info.Source?.name ?? "Unknown"}" +
            $"for {info.Amount} damage. " +
            $"HP: {_health.CurrentHealth}/{_health.MaxHealth}");
            _animDriver.TriggerHit();
        }
    }
    private void HandleDied()
    {
        Debug.Log("Dummy is dead, resetting");
        if (_autoReset)
        {
            Invoke(nameof(ResetDummy), _resetDelay);
            _animDriver.TriggerDie();
        }
    }

    private void ResetDummy()
    {
        _health.ResetHealth();
    }
}
