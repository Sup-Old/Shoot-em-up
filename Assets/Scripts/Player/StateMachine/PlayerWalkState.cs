using UnityEngine;

public class PlayerWalkState : MonoBehaviour, IPlayerState
{
    [SerializeField] private PlayerAnimator _animator;
    public void Enter()
    {
        _animator.PlayWalk();
    }

    public void Exit()
    {
    }

    public void OnUpdate()
    {

    }
}
