using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerExperience _playerExperience;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private int _enemyLayerID = 6; // ������: ���� "Enemy"
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerStateMachine _stateMachine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��������� ��� ��������� null-������
        if (collision == null || _health == null) return;

        // �������� ���� (����������������)
        if (collision.gameObject.layer == _enemyLayerID)
        {
            // ���������� ��������� Enemy
            if (collision.TryGetComponent(out Enemy enemy) && enemy.Config != null)
            {
                _health.Decrease(enemy.Config.Damage);
            }
            else
            {
                Debug.LogWarning($"Enemy ��� Config �� ������ � {collision.name}", this);
            }
        }
    }

    private void Update()
    {
        if (_input == null || _stateMachine == null) return;

        // ���������������� �������� ��������
        _stateMachine.SetState(_input.MoveDir != Vector2.zero
            ? _stateMachine.GetState<PlayerWalkState>()
            : _stateMachine.GetState<PlayerIdleState>());
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // �������������� ���� ������ � ���������
        if (_enemyLayerID == 0 && LayerMask.NameToLayer("Enemy") != -1)
        {
            _enemyLayerID = LayerMask.NameToLayer("Enemy");
        }
    }
#endif
}