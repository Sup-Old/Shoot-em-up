using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IPlayerState
{
    [SerializeField] private PlayerAnimator _animator;
    public void Enter()
    {
        _animator.PlayAttack();
    }

    public void Exit()
    {
    }

    public void OnUpdate()
    {
    }

}
