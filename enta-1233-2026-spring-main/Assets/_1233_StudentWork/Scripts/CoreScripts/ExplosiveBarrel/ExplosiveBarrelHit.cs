using UnityEngine;

public class ExplosiveBarrelHit : MonoBehaviour, IDamageReciever
{
    [SerializeField] private SphereOverlapNonAlloc _overlap;

    private void Awake()
    {
        
    }

    public void ApplyDamage(DamageInfo info)
    {
        _overlap.CheckOverlap();
    }
}
