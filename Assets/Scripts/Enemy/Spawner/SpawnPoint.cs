using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private EnemyPool _enemies;
    [SerializeField] private EnemyConfig _config;
    private GameObject _currentEnemy;

    private void OnEnable()
    {
        _timer.SetTimer(_config.SpawnTime);
        _timer.OnTimeEnd += SpawnEnemy;
    }

    private void OnDisable()
    {
        _timer.OnTimeEnd -= SpawnEnemy;
    }

    private void Update()
    {
        if (_currentEnemy == null || !_currentEnemy.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            return;
        }
        if (enemyHealth.IsDead)
        {
            _currentEnemy.transform.position = transform.position;
            _currentEnemy.SetActive(false);
            _timer.SetTimer(_config.SpawnTime);
        }
    }

    private void SpawnEnemy()
    {
        _currentEnemy = _enemies.CreateEnemie();
    }
}
