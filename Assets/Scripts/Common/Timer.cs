using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timeRemaining;

    public event Action OnTimeEnd;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
        }
        else
        {
            _timeRemaining = 0;
            OnTimeEnd?.Invoke();
            enabled = false;
        }
    }

    public void SetTimer(float time)
    {
        _timeRemaining = time;
        enabled = true;
    }
}

