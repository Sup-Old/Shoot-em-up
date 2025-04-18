using System;
using UnityEngine;

public class CampFireHealth : MonoBehaviour, IHealth
{
    [SerializeField] private CampFireConfig _config;

    public event Action OnDecrease;
    public event Action OnIncrease;
    public event Action<float> OnHealthChanged;

    private float _current;
    public float Current
    {
        get => _current;
        private set
        {
            float oldValue = _current;
            _current = Mathf.Clamp(value, _config.MinHP, _config.MaxHP);

            if (Mathf.Abs(oldValue - _current) > 0.001f)
            {
                //Debug.Log($"[CampFireHealth] Health изменился с {oldValue} на {_current}");
                OnHealthChanged?.Invoke(GetHealthPercent());
            }
        }
    }

    private void Start()
    {
        Debug.Log($"[CampFireHealth] Start() | MaxHP: {_config.MaxHP}, MinHP: {_config.MinHP}");
        Current = _config.MaxHP;
        Debug.Log($"[CampFireHealth] Текущее здоровье: {Current}");
    }

    public float GetHealthPercent()
    {
        float percent = Mathf.InverseLerp(_config.MinHP, _config.MaxHP, Current);
        percent = (float)System.Math.Round(percent, 2); // Округление до 2 знаков
        //Debug.Log($"[Health] Percent: {percent:F2}");
        return percent;
    }

    public void Decrease(float value)
    {
        //Debug.Log($"[CampFireHealth] Decrease({value}) | До: {Current}");

        if (value <= 0)
        {
            Debug.LogWarning("[CampFireHealth] Попытка уменьшить на 0 или отрицательное значение!");
            return;
        }

        Current -= value;
        OnDecrease?.Invoke();

        //Debug.Log($"[CampFireHealth] После Decrease: {Current}");
    }

    public void Increase(float value)
    {
        //Debug.Log($"[CampFireHealth] Increase({value}) | До: {Current}");

        if (value <= 0)
        {
            Debug.LogWarning("[CampFireHealth] Попытка увеличить на 0 или отрицательное значение!");
            return;
        }

        Current += value;
        OnIncrease?.Invoke();

        //Debug.Log($"[CampFireHealth] После Increase: {Current}");
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Debug.Log("[CampFireHealth] OnValidate()");
        if (_config == null)
            Debug.LogError("Config не назначен!");
    }
#endif
}