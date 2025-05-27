using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSentry : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private WeaponStateMachine _weaponStateMachine;
    [SerializeField] private PlayerStateMachine _playerStateMachine;

    [SerializeField] private Transform _meleeWeaponHolder;
    [SerializeField] private Transform _rangeWeaponHolder;

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

    public void ChangeWeapon(GameObject newWeapon, MeleeWeaponConfig config)
    {
        Transform spawnPoint = _meleeWeaponHolder;

        var spawnedWeapon = Instantiate(newWeapon, spawnPoint.position, Quaternion.identity);
        spawnedWeapon.transform.parent = spawnPoint.parent;
        Destroy(spawnPoint.gameObject);
        _meleeWeaponHolder = spawnedWeapon.transform;
        gameObject.GetComponent<DummyState>().Config = config;
        _weapons.Add(spawnedWeapon.GetComponent<IWeapon>());
    }

    public void ChangeWeapon(GameObject newWeapon, RangeWeaponConfig config)
    {
        Transform spawnPoint = _rangeWeaponHolder;

       /* var spawnedWeapon = Instantiate(newWeapon, spawnPoint.position, Quaternion.identity);
        spawnedWeapon.transform.parent = spawnPoint.parent;
        Destroy(spawnPoint.gameObject);
        _rangeWeaponHolder = spawnedWeapon.transform;*/

        spawnPoint.GetComponent<SpriteRenderer>().sprite = newWeapon.GetComponentInChildren<SpriteRenderer>().sprite;

        StoneState stoneState = gameObject.GetComponent<StoneState>();
        stoneState._config = config;
        //_weapons.Add(spawnedWeapon.GetComponent<IWeapon>());

        if (stoneState != null && stoneState._bulletPool != null)
        {
            stoneState._bulletPool.UpdateBulletPrefab(newWeapon.GetComponent<Bullet>());
        }
    }

}
