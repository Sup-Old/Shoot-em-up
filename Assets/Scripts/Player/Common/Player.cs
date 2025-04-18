using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerExperience _playerExperience;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private int _enemyLayerID = 6; // Пример: слой "Enemy"
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerStateMachine _stateMachine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем все возможные null-ссылки
        if (collision == null || _health == null) return;

        // Проверка слоя (оптимизированная)
        if (collision.gameObject.layer == _enemyLayerID)
        {
            // Безопасное получение Enemy
            if (collision.TryGetComponent(out Enemy enemy) && enemy.Config != null)
            {
                _health.Decrease(enemy.Config.Damage);
            }
            else
            {
                Debug.LogWarning($"Enemy или Config не найден у {collision.name}", this);
            }
        }
    }

    private void Update()
    {
        if (_input == null || _stateMachine == null) return;

        // Оптимизированная проверка движения
        _stateMachine.SetState(_input.MoveDir != Vector2.zero
            ? _stateMachine.GetState<PlayerWalkState>()
            : _stateMachine.GetState<PlayerIdleState>());
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Автозаполнение слоя врагов в редакторе
        if (_enemyLayerID == 0 && LayerMask.NameToLayer("Enemy") != -1)
        {
            _enemyLayerID = LayerMask.NameToLayer("Enemy");
        }
    }
#endif
}