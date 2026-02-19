using UnityEngine;

public class BudBrain : MonoBehaviour
{
    public enum FireMode
    {
        FiredAxis,
        DirectAim,
        ArcFire
    }

    [Header("Components")]
    [SerializeField] private Health _health;

    [SerializeField] private ProjectileWeapon _weapon;
    [SerializeField] private DetectionSystem _detection;
    [SerializeField] private RotateToTarget _rotater;
    [SerializeField] private EnemyAnimDriver _animatior;

    [Header ("Settings")]
    [SerializeField] private FireMode _mode = FireMode.DirectAim;

    [SerializeField] private Vector3 _fixedAxis = Vector3.forward;

    private ITargetProvider _targetProvider;

    private void Awake()
    {
        _targetProvider = GetComponent<ITargetProvider>();
        if (_health == null) _health = GetComponent<Health>();
        if (_animatior == null) _animatior = GetComponent<EnemyAnimDriver>();
    }

    private void Update()
    {
        if (_health != null && _health.IsDead) return;

        switch (_mode)
        {
            case FireMode.FiredAxis:
                if (_weapon.CanFire)
                    _animatior?.TriggerAttack();
                _weapon.Fire(transform.TransformDirection(_fixedAxis), true);
                break;

            case FireMode.DirectAim:
                HandleDirectAim();
                break;

            case FireMode.ArcFire:
                HandleArcFire();
                break;
        }
    }

    private void OnEnable()
    {
        if (_health !=null) _health.OnDied += HandleDied;
    }

    private void OnDisable()
    {
        if (_health !=null) _health.OnDied -= HandleDied;
    }

    private void HandleDirectAim()
    {
        if (_targetProvider == null || !_targetProvider.HasTarget) return;

        var target = _targetProvider.GetTarget();
        var targetPos = _targetProvider.GetTargetPosition();

        if (_detection.IsTargetInDetectionRange(target) && _detection.HasLineOfSight(target))
        {
            _rotater?.FacePosition(targetPos);
            if (_weapon.CanFire)
            {
                _animatior?.TriggerAttack();
                _weapon.Fire(targetPos);
            }
        }
    }

    private void HandleArcFire()
    {
        if (_targetProvider == null || !_targetProvider.HasTarget) return;

        var target = _targetProvider.GetTarget();
        var targetPos = _targetProvider.GetTargetPosition();

        if (_detection.IsTargetInDetectionRange(target) && _detection.HasLineOfSight(target))
        {
            _rotater?.FacePosition(targetPos);
            if (_weapon.CanFire)
            {
                _animatior?.TriggerAttack();
                _weapon.Fire(targetPos);
            }
        }
    }

    private void HandleDied()
    {
        enabled = false;
    }
}
