using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private EnemyPool _enemies;
    [SerializeField] private EnemyConfig _config;
    private Enemy _currentEnemy;

    private void OnEnable()
    {
        _timer.SetTimer(_config.SpawnTime);
        _timer.OnTimeEnd += SpawnEnemy;
    }

    private void OnDisable()
    {
        _timer.OnTimeEnd -= SpawnEnemy;
        if (_currentEnemy != null && _currentEnemy.Config != null)
        {
            var health = _currentEnemy.GetComponent<EnemyHealth>();
            if (health != null)
                health.OnDeath -= OnEnemyDeath;
        }
    }

    private void SpawnEnemy()
    {
        _currentEnemy = _enemies.CreateEnemy();
        _currentEnemy.transform.position = transform.position;
        _currentEnemy.gameObject.SetActive(true);

        // подписка на смерть
        var health = _currentEnemy.GetComponent<EnemyHealth>();
        health.ToFull();
        if (health != null)
        {
            
            health.OnDeath -= OnEnemyDeath; // на всякий случай удалим дубли
            health.OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        // отключаем врага после смерти, запускаем таймер
        if (_currentEnemy != null)
        {
            _currentEnemy.gameObject.SetActive(false);
            _timer.SetTimer(_config.SpawnTime);
        }
    }
}
