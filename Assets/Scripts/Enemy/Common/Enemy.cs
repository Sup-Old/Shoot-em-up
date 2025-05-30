using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _rangeWeaponLayerID;
    [SerializeField] private int _meleeWeaponLayerID;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyConfig _config;

    public EnemyConfig Config => _config;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _rangeWeaponLayerID &&
            collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            _health.Decrease(bullet.Config?.WeaponDamage ?? 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _meleeWeaponLayerID &&
            collision.gameObject.TryGetComponent(out DummyState melee))
        {
            _health.Decrease(melee.Config?.WeaponDamage ?? 0);
        }
    }
}
