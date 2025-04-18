using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private Dictionary<Type, IPlayerState> _states = new();
    private IPlayerState _currentState;

    private void Awake()
    {
        var activeStates = GetComponents<IPlayerState>();
        foreach(var state in activeStates)
        {
            _states.Add(state.GetType(), state);
        }
        SetState(GetState<PlayerIdleState>());
    }

    private void FixedUpdate()
    {
        _currentState.OnUpdate();
    }

    public void SetState(IPlayerState playerState)
    {
        _currentState?.Exit();
        _currentState = playerState;
        _currentState.Enter();
    }

    public IPlayerState GetState<T>() where T : IPlayerState
    {
        var type = typeof(T);
        return _states[type];
    }
}
