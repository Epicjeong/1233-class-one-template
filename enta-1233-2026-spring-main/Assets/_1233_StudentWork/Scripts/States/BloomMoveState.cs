using UnityEngine;

public class BloomMoveState : EnemyState
{
    private readonly BloomBrain _brain;

    public BloomMoveState(BloomBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Tick()
    {
        var target = _brain.TargetProvider.GetTarget();
        if (target == null) return;

        var distance = Vector3.Distance(_brain.transform.position, target.position);
        var hasLOS = _brain.DetectionSystem.HasLineOfSight(target);

        if (hasLOS && distance <= _brain.AttackRange)
        {
            Machine.ChangeState(new BloomAttackState(_brain, Machine));
            _brain.Mover.Stop();
            return;
        }

        _brain.Mover?.SetDestination(target.position);
        if (_brain.Mover != null) 
            _brain.AnimDriver.SetSpeed(_brain.Mover.Velocity.magnitude);
        else
            _brain.AnimDriver.SetSpeed(0);
    }
}
