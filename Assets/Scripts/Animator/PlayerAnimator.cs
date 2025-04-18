using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayWalk()
    {
        _animator.SetTrigger("transitionToWalk");
    }

    public void PlayIdle()
    {
        _animator.SetTrigger("transitionToIdle");
    }

    public void PlayAttack()
    {
        _animator.SetTrigger("transitionToAttack");
    }

}
