using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    public void ExplosionDamage(Collider collision)
    {
        var damageReciever = collision.gameObject.GetComponent<IDamageReciever>();
        if (damageReciever != null)
        {
            var info = new DamageInfo
            {
                Amount = _damage
            };
            damageReciever.ApplyDamage(info);
        }
    }
}
