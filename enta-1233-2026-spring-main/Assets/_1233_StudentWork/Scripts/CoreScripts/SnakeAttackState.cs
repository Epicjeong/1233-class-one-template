using UnityEngine;

public class SnakeAttackState : EnemyState
{
    private readonly SnakeBrain _brain;
    private float _exitTime;

    public SnakeAttackState(SnakeBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Enter()
    {
        _brain._mover.Stop();
        _brain.AnimDriver.SetSpeed(0);
        _brain.AnimDriver.TriggerAttack();

        _exitTime = Time.time + _brain.AttackCooldown;

        ApplyMeleeDamage();
    }

    public override void Tick()
    {
        var target = _brain._targetProvider.GetTarget();
        var targetPos = _brain._targetProvider.GetTargetPosition();
        if (target != null) _brain.Rotater.FacePosition(targetPos);

        if (Time.time > _exitTime) Machine.ChangeState(new SnakeChaseState(_brain, Machine));
    }

    private void ApplyMeleeDamage()
    {
        var target = _brain._targetProvider.GetTarget();
        if (target == null) return;

        if (Vector3.Distance(_brain.transform.position, target.position) <= _brain.AttackRange + 0.5f)
        {
            var reciever = target.GetComponent<IDamageReciever>();
            if (reciever != null)
            {
                reciever.ApplyDamage
                    (
                    new DamageInfo
                    {
                        Amount = _brain.Damage,
                        Source = _brain.gameObject,
                        HitPoint = target.position,
                        HitNormal = Vector3.up
                    });
            }
        }
    }


}
