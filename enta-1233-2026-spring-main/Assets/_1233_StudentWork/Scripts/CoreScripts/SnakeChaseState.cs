using UnityEngine;

public class SnakeChaseState : EnemyState
{
    private readonly SnakeBrain _brain;

    public SnakeChaseState(SnakeBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Tick()
    {
        var target = _brain._targetProvider.GetTarget();
        if (target == null) return;

        _brain?._mover.SetDestination(target.position);

        if (_brain._mover != null)
            _brain.AnimDriver.SetSpeed(_brain._mover.Velocity.magnitude);
        else
            _brain.AnimDriver.SetSpeed(0);

        var distance = Vector3.Distance(_brain.transform.position, target.position);
        if (distance <= _brain.AttackRange) Machine.ChangeState(new SnakeAttackState(_brain, Machine));
    }
}
