using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour, IExperience
{
    public event Action OnIncrease;

    [SerializeField] private Slider experienceSlider; // Привязать через инспектор
    public int Current { get; private set; }
    public int MaxExperience { get; private set; } = 100; // Можно настроить через инспектор

    private void Start()
    {
        Current = 0;

        if (experienceSlider != null)
        {
            experienceSlider.minValue = 0;
            experienceSlider.maxValue = MaxExperience;
            experienceSlider.value = Current;
        }
    }

    public void Increase(int value)
    {
        Current += value;

        //if (Current >= MaxExperience)
        //{
        //    Current = 0; // Сброс опыта
        //    MaxExperience += 50; // Усложнение следующего уровня
        //    OnLevelUp();
        //}

        if (experienceSlider != null)
        {
            experienceSlider.maxValue = MaxExperience;
            experienceSlider.value = Current;
        }

        OnIncrease?.Invoke();
    }

  
}
