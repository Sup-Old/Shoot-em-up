using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyConfig _config;

    public event Action OnDecrease;

    public bool IsDead { get; private set; }
    public float Current { get; private set; }
    public int ExpReward { get; private set; }

    private void OnEnable()
    {
        IsDead = false;
        Current = _config.MaxHealthPoints;
        ExpReward = _config.ExpReward;
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
            GiveExperience();
            Destroy(gameObject);
        }

        OnDecrease?.Invoke();
    }

    private void GiveExperience()
    {
        var playerExp = FindObjectOfType<PlayerExperience>();
        if (playerExp != null)
        {
            Debug.Log("Give experience= " + ExpReward);
            playerExp.Increase(ExpReward);
        }
        else
        {
            Debug.LogWarning("Не найден PlayerExperience в сцене!");
        }
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
