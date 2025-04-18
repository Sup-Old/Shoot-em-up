using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int _poolSise;
    [SerializeField] private bool _isAutoExpand;
    [SerializeField] private Enemy _prefab;
    private ObjectPool<Enemy> _enemies;

    public int PoolSise { get { return _poolSise; } }

    private void Start()
    {
        _enemies = new ObjectPool<Enemy>(_prefab, _isAutoExpand, transform, _poolSise);
    }

    public GameObject CreateEnemie()
    {
        return _enemies.GetFreeElement().gameObject;
    }
}
