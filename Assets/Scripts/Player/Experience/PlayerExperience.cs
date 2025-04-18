using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour, IExperience
{
    public event Action OnIncrease;

    public int Current { get; private set; }

    private void Start()
    {
        Current = 0;
    }

    public void Increase(int value)
    {
        Current += value;
        OnIncrease?.Invoke();
    }
}
