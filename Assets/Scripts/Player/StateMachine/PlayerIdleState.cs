using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IPlayerState
{
    [SerializeField] private PlayerAnimator _animator;
    public void Enter()
    {
        _animator.PlayIdle();
    }

    public void Exit()
    {
    }

    public void OnUpdate()
    {
    }
}
