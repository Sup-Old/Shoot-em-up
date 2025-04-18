using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{

    [SerializeField] private EnemyConfig _config;

    public event Action OnDecrease;

    public bool IsDead { get; private set; }
    public float Current { get; private set; }

    private void OnEnable()
    {
        IsDead = false;
        Current = _config.MaxHealthPoints;
    }

    private void OnDisable()
    {
        IsDead = false;
    }

    public void Decrease(float value)
    {
        Current -= value;

        if (Current <= _config.MinHealthPoints)
        {
            Current = _config.MinHealthPoints;
            IsDead = true;
        }

        OnDecrease?.Invoke();
    }

    public void Increase(float value)
    {
        Current += value;

        if (Current >= _config.MaxHealthPoints)
        {
            Current = _config.MaxHealthPoints;
        }
    }
}
