using UnityEngine;
using static BudBrain;

public class BloomBrain : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EnemyStateMachine _stateMachine;

    [SerializeField] private Health _health;
    [SerializeField] private ProjectileWeapon _weapon;
    [SerializeField] private DetectionSystem _detection;
    [SerializeField] private RotateToTarget _rotater;
    [SerializeField] private EnemyAnimDriver _animDriver;

    [Header("Settings")]
    [SerializeField] private float _attackRange = 10f;

    [SerializeField] private float _stopRange = 8f;

    public IMover Mover { get; private set; }
    public DetectionSystem DetectionSystem => _detection;
    public EnemyAnimDriver AnimDriver => _animDriver;
    public RotateToTarget Rotater => _rotater;
    public ITargetProvider TargetProvider { get; private set; }
    public float AttackRange => _attackRange;
    public ProjectileWeapon Weapon => _weapon;
    public float StopRange => _stopRange;

    private void Awake()
    {
        TargetProvider = GetComponent<ITargetProvider>();
        Mover = GetComponent<IMover>();
        if (_stateMachine == null) _stateMachine = GetComponent<EnemyStateMachine>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _stateMachine.Initialize(new BloomMoveState(this, _stateMachine));
    }

    private void OnEnable()
    {
        if (_health != null) _health.OnDied += HandleDied;
    }

    private void OnDisable()
    {
        if (_health != null) _health.OnDied -= HandleDied;
    }

    private void HandleDied()
    {
        _stateMachine.ChangeState(null);
        if (Mover != null)
        {
            Mover.Stop();
            Mover.Enabled(false);
        }
        _animDriver.TriggerDie();
        enabled = false;
    }
}
