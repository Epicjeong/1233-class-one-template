using UnityEngine;

public class ExplosiveBarrelHit : MonoBehaviour, IDamageReciever
{
    [SerializeField] private AudioSource _explosion;
    [SerializeField] private SphereOverlapNonAlloc _overlap;
    #region Particle
    [SerializeField] private GameObject _particles;

    void SpawnImpact(Vector3 position)
    {
        Instantiate(_particles, position, Quaternion.identity);
    }
    #endregion

    public void ApplyDamage(DamageInfo info)
    {
        _overlap.CheckOverlap();
        SpawnImpact(transform.position);
        _explosion?.Play();
        Destroy(gameObject);
    }
}
