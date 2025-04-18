using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Collider2D _attackTrigger;
    [SerializeField] private EnemyConfig _config;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private TriggerAttackSentry _attackSentry;

    private void Awake()
    {
        _attackTrigger.enabled = false;
    }

    public EnemyConfig Config { get { return _config; } }

    private void OnEnable()
    {
        _attackSentry.OnZoneEnter += StartAttack;
        _attackSentry.OnZoneExit += StopAttack;
    }
    
    private void OnDisable()
    {
        _attackSentry.OnZoneEnter -= StartAttack;
        _attackSentry.OnZoneExit -= StopAttack;
    }    

    public void StartAttack()
    {
        StartCoroutine(AttackRoutine());
        _attackTrigger.enabled = true;
        StartCoroutine(Hide());
        _animator.PlayAttack();
    }

    private void StopAttack()
    {
        StopAllCoroutines();
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_config.AttackCoolDown);
        StartAttack();
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.5f);
        _attackTrigger.enabled = false;
        StopCoroutine(Hide());
    }
}
