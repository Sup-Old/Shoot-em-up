using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateMachine : MonoBehaviour
{
    private Dictionary<Type, IWeaponState> _states = new();
    private IWeaponState _currentState;

    private void Awake()
    {
        var activeStates = GetComponents<IWeaponState>();
        foreach (var state in activeStates)
        {
            _states.Add(state.GetType(), state);
        }
        SetState(GetState<DummyState>());
    }

    private void FixedUpdate()
    {
        _currentState.OnUpdate();
    }

    public void SetState(IWeaponState weaponState)
    {
        _currentState?.Exit();
        _currentState = weaponState;
        _currentState.Enter();
    }

    public IWeaponState GetState<T>() where T : IWeaponState
    {
        var type = typeof(T);
        return _states[type];
    }

    public Type GetCurrentStateType()
    {
        return _states[_currentState.GetType()].GetType();
    }
}