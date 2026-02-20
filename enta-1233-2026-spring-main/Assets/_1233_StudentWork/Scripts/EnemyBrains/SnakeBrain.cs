using NUnit.Framework.Constraints;
using UnityEngine;

public class SnakeBrain : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private EnemyStateMachine _stateMachine;

    [SerializeField] private Health _health;
    [SerializeField] private DetectionSystem _detection;
    [SerializeField] private RotateToTarget _rotater;
    [SerializeField] private EnemyAnimDriver _animDriver;

    [Header("Settings")]
    [SerializeField] private float _attackRange = 2f;

    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private int _damage = 15;

    public IMover _mover {  get; private set; }

    public DetectionSystem DetectionSystem => _detection;
    public EnemyAnimDriver AnimDriver => _animDriver;
    public RotateToTarget Rotater => _rotater;
    public ITargetProvider _targetProvider { get; private set; }
    public float AttackRange => _attackRange;
    public float AttackCooldown => _attackCooldown;
    public int Damage => _damage;

    private void Awake()
    {
        _targetProvider = GetComponent<ITargetProvider>();
        _mover = GetComponent<IMover>();
        if (_stateMachine == null) GetComponent<EnemyStateMachine>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _stateMachine.Initialize(new SnakeChaseState(this, _stateMachine));
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
        if (_mover != null)
        {
            _mover.Stop();
            _mover.Enabled(false);
        }
    }
}
