using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour, IExperience
{
    public event Action OnIncrease;
    public event Action<float> OnDecrease;

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

    public void Decrease(int value)
    {
        Current -= value;
        OnDecrease?.Invoke(0);
    }
}
