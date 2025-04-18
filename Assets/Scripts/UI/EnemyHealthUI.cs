using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthUI;
    [SerializeField] private EnemyConfig _config;
    [SerializeField] private EnemyHealth _health;

    private void OnEnable()
    {
        _health.OnDecrease += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _health.OnDecrease -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        _healthUI.fillAmount = _health.Current / _config.MaxHealthPoints;
        if (_healthUI.fillAmount == _config.MinHealthPoints)
        {
            ReloadHealthBar();
        }
    }

    private void ReloadHealthBar()
    {
        _healthUI.fillAmount = _config.MaxHealthPoints;
    }
}
