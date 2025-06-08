using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyConfig _config;

    public event Action OnDecrease;
    public event Action OnDeath;
    public event Action<float> OnHealthChanged; //  добавлено

    public bool IsDead { get; private set; }
    public float Current { get; private set; }
    public int ExpReward { get; private set; }

    private void OnEnable()
    {
        IsDead = false;
        Current = _config.MaxHealthPoints;
        ExpReward = _config.ExpReward;

        //  уведомляем интерфейс
        OnHealthChanged?.Invoke(GetHealthPercent());
    }

    private void OnDisable()
    {
        IsDead = false;
    }

    public void Decrease(float value)
    {
        Current -= value;

        if (Current <= _config.MinHealthPoints && !IsDead)
        {
            Current = _config.MinHealthPoints;
            IsDead = true;

            GiveExperience();
            OnDeath?.Invoke();

            gameObject.SetActive(false);
        }

        OnDecrease?.Invoke();
        OnHealthChanged?.Invoke(GetHealthPercent()); //  обновляем UI
    }

    public void Increase(float value)
    {
        Current += value;
        if (Current >= _config.MaxHealthPoints)
            Current = _config.MaxHealthPoints;

        OnHealthChanged?.Invoke(GetHealthPercent()); //  обновляем UI
    }

    public float GetHealthPercent()
    {
        return Mathf.InverseLerp(_config.MinHealthPoints, _config.MaxHealthPoints, Current);
    }

    private void GiveExperience()
    {
        var playerExp = FindObjectOfType<PlayerExperience>();
        if (playerExp != null)
        {
            playerExp.Increase(ExpReward);
        }
    }
    public void ToFull() 
    { 
        Current = _config.MaxHealthPoints;
        OnDecrease?.Invoke();
        OnHealthChanged?.Invoke(GetHealthPercent());
    }
}
