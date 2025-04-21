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
        if (collision.gameObject.layer != _rangeWeaponLayerID)
            return;

        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            if (bullet.Config != null)
            {
                _health.Decrease(bullet.Config.WeaponDamage);
            }
            else
            {
                Debug.LogWarning("У пули не назначен Config!", bullet.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != _meleeWeaponLayerID)
            return;

        if (collision.gameObject.TryGetComponent(out DummyState melee))
        {
            if (melee.Config != null)
            {
                _health.Decrease(melee.Config.WeaponDamage);
            }
            else
            {
                Debug.LogWarning("У оружия ближнего боя не назначен Config!", melee.gameObject);
            }
        }
    }
}