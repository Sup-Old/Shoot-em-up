using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private PlayerConfig _config;

    public event Action<float> OnDecrease;
    public event Action OnIncrease;

    public float Current { get; private set; }

    private void Start()
    {
        Current = _config.MaxHP;
    }

    public void Decrease(float value)
    {
        Current -= value;
        if (Current <= _config.MinHP)
        {
            Current = _config.MinHP;
        }
        OnDecrease?.Invoke(_config.MinHP);
    }

    public void Increase(float value)
    {
        Current += value;
        if (Current >= _config.MaxHP)
        {
            Current = _config.MaxHP;
        }
        OnIncrease?.Invoke();
    }
}
