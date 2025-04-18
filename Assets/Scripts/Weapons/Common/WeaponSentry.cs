using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSentry : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private WeaponStateMachine _weaponStateMachine;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    private List<IWeapon> _weapons = new();

    private void Awake()
    {
        var activeComponents = GetComponents<IWeapon>();
        foreach (var component in activeComponents)
        {
            _weapons.Add(component);
        }
    }

    private void OnEnable()
    {
        _input.OnAttacked += Attack;
        _input.OnWeaponChanged += Change;
    }

    private void OnDisable()
    {
        _input.OnAttacked -= Attack;
        _input.OnWeaponChanged -= Change;
    }

    public void Change()
    {
        if (_weaponStateMachine.GetCurrentStateType() != typeof(DummyState))
        {
            _weaponStateMachine.SetState(_weaponStateMachine.GetState<DummyState>());
            return;
        }
        _weaponStateMachine.SetState(_weaponStateMachine.GetState<StoneState>());
    }

    private void Attack()
    {
        foreach (var weapon in _weapons)
        {
            if (weapon.GetType() == _weaponStateMachine.GetCurrentStateType())
            {
                weapon.Attack();
                _playerStateMachine.SetState(_playerStateMachine.GetState<PlayerAttackState>());
                return;
            }
        }
    }
}
