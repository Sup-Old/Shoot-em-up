using UnityEngine;

public class DummyState : MonoBehaviour,IWeaponState, IWeapon
{
    [SerializeField] private GameObject _currentWeapon;
    [SerializeField] private BoxCollider2D _meleeWeaponCollider;
    [SerializeField] private MeleeWeaponConfig _config;
    [SerializeField] private Timer _timer;
    private bool _isAttacked;

    public MeleeWeaponConfig Config { get { return _config; } }

    private void Awake()
    {
        _meleeWeaponCollider.enabled = false;
    }

    private void OnDisable()
    {
        _timer.OnTimeEnd -= Hide;
    }

    public void Attack()
    {
        if (_isAttacked)
        {
            return;
        }
        _isAttacked = true;
        _timer.OnTimeEnd += Hide;
        _timer.SetTimer(_config.AttackCoolDown);
        _meleeWeaponCollider.enabled = true;
    }

    public void Hide()
    {
        _isAttacked = false;
        _meleeWeaponCollider.enabled = false;
    }

    public void Enter()
    {
        _currentWeapon.SetActive(true);
    }

    public void Exit()
    {
        _currentWeapon.SetActive(false);
    }

    public void OnUpdate()
    {
    }
}
