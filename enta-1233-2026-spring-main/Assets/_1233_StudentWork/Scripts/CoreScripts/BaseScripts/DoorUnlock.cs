using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public enum UnlockMethod
    {
        hit,
        kill
    }

    [SerializeField] private Health _health;
    [SerializeField] private LockedDoor _door;

    [SerializeField] private UnlockMethod _unlock = UnlockMethod.kill;

    public void OnEnable()
    {
        if (_health != null)
        {
            switch (_unlock)
            {
                case UnlockMethod.hit:
                    _health.OnDamaged += HandleDamaged;
                    break;
                case UnlockMethod.kill:
                    _health.OnDied += HandleDied;
                    break;
            }
        }
    }

    public void HandleDamaged(DamageInfo info)
    {
        _door.GetKeys();
    }
    public void HandleDied()
    {
        _door.GetKeys();
    }
}
