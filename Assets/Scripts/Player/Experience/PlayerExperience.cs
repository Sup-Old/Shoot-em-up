using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour, IExperience
{
    public event Action OnIncrease;

    [SerializeField] private Slider experienceSlider; // ��������� ����� ���������
    public int Current { get; private set; }
    public int MaxExperience { get; private set; } = 100; // ����� ��������� ����� ���������

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
        //    Current = 0; // ����� �����
        //    MaxExperience += 50; // ���������� ���������� ������
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
