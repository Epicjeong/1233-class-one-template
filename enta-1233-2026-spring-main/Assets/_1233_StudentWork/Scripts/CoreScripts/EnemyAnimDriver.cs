using UnityEngine;

public class EnemyAnimDriver : MonoBehaviour
{
    public static readonly int SpeedHash = Animator.StringToHash("Speed");
    public static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    public static readonly int AttackTriggerHash = Animator.StringToHash("Attack");
    public static readonly int HitTriggerHash = Animator.StringToHash("Hit");
    public static readonly int DieTriggerHash = Animator.StringToHash("Die");
    public static readonly int MoveTriggerHash = Animator.StringToHash("Move");
    [SerializeField] private Animator _animator;

    public void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }

    public void SetSpeed(float speed)
    {
        if (_animator == null) return;
        _animator.SetFloat(SpeedHash, speed);
        _animator.SetBool(IsMovingHash, speed > 0.1f);
    }

    public void TriggerAttack()
    {
        if (_animator == null) return;
        _animator.SetTrigger(AttackTriggerHash);
    }

    public void TriggerHit()
    {
        if (_animator == null) return;
        _animator.SetTrigger(HitTriggerHash);
    }

    public void TriggerDie()
    {
        if (_animator == null) return;
        _animator.SetTrigger(DieTriggerHash);
    }
    public void TriggerMove()
    {
        if (_animator == null) return;
        _animator.SetTrigger(MoveTriggerHash);
    }
}
