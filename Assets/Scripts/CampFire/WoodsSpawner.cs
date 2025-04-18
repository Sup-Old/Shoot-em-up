using UnityEngine;

public class WoodsSpawner : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnTimeMax;
    private float _spawnTime;

    private void Start()
    {
        _spawnTime = _spawnTimeMax;
    }

    private void Update()
    {
        _spawnTime -= Time.deltaTime;
        if(_spawnTime <= 0)
        {
            _spawnTime = _spawnTimeMax;
            SpawnWoods();
        }
    }

    private void SpawnWoods()
    {
        var spawnPoint = new Vector2(Random.Range(_collider.bounds.min.x, _collider.bounds.max.x), Random.Range(_collider.bounds.min.y, _collider.bounds.max.y));
        Instantiate(_prefab, spawnPoint, Quaternion.identity);
    }
}
