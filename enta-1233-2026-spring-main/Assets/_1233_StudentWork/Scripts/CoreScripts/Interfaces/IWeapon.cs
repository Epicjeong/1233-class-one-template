using UnityEngine;

public interface IWeapon
{
    bool canFire { get; }
    void Fire(Vector3 targetPosition);
    void Fire(Vector3 direction, bool useDirection);
}
