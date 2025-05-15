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
        if (IsDead) return;

        Current -= value;

        if (Current <= _config.MinHealthPoints)
        {
            Die();
        }

        OnDecrease?.Invoke();
    }

    private void Die()
    {
        IsDead = true;
        Current = _config.MinHealthPoints;

        
        PlayerExperience playerExp = FindObjectOfType<PlayerExperience>();
        if (playerExp != null)
        {
            playerExp.Increase(_config.ExperienceReward);
        }

        Destroy(gameObject);
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
