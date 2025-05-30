using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int _poolSise;
    [SerializeField] private bool _isAutoExpand;
    [SerializeField] private Enemy _prefab;
    private ObjectPool<Enemy> _enemies;

    public int PoolSise => _poolSise;

    private void Start()
    {
        _enemies = new ObjectPool<Enemy>(_prefab, _isAutoExpand, transform, _poolSise);
    }

    public Enemy CreateEnemy()
    {
        return _enemies.GetFreeElement();
    }
}
