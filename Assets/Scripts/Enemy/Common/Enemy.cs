using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _rangeWeaponLayerID;
    [SerializeField] private int _meleeWeaponLayerID;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyConfig _config;

    public EnemyConfig Config { get { return _config; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != _rangeWeaponLayerID)
        {
            return;
        }
        collision.gameObject.TryGetComponent<Bullet>(out var bullet);
        _health.Decrease(bullet.Config.WeaponDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != _meleeWeaponLayerID)
        {
            return;
        }
        collision.gameObject.TryGetComponent<DummyState>(out var melee);
        _health.Decrease(melee.Config.WeaponDamage);
    }
}
