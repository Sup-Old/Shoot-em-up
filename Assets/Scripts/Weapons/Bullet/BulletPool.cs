using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _poolSize = 2;
    [SerializeField] private bool _isAutoExpand;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private Transform _spawnPoint;
    private ObjectPool<Bullet> _bullets;

    private void Start()
    {
        _bullets = new ObjectPool<Bullet>(_prefab, _isAutoExpand, _spawnPoint, _poolSize);
    }

    public Rigidbody2D CreateBullet()
    {
        var bullet = _bullets.GetFreeElement();
        if (!bullet.TryGetComponent<Rigidbody2D>(out var bulletRigidBody))
        {
            return null;
        }
        return bulletRigidBody;
    }
}
