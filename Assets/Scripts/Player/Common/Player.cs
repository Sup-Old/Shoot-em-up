using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerExperience _playerExperience;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private int _enemyLayerID = 6;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerStateMachine _stateMachine;
    [SerializeField] private PlayerFlip _playerFlip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || _health == null) return;

        if (collision.gameObject.layer == _enemyLayerID)
        {
 
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

    public void ChangeSprite(Sprite playerSkin0, Sprite playerSkin1)
    {
        _playerFlip._sprites[0] = playerSkin0;
        _playerFlip._sprites[1] = playerSkin1;
        _stateMachine.gameObject.GetComponent<SpriteRenderer>().sprite = playerSkin0;
    }


    private void Update()
    {
        if (_input == null || _stateMachine == null) return;

        _stateMachine.SetState(_input.MoveDir != Vector2.zero
            ? _stateMachine.GetState<PlayerWalkState>()
            : _stateMachine.GetState<PlayerIdleState>());
    }

#if UNITY_EDITOR
    private void OnValidate()
    {

        if (_enemyLayerID == 0 && LayerMask.NameToLayer("Enemy") != -1)
        {
            _enemyLayerID = LayerMask.NameToLayer("Enemy");
        }
    }
#endif
}