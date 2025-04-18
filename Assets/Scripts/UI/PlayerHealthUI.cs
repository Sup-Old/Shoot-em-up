using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthUI;
    [SerializeField] private Image _campFireHealthUI;
    [SerializeField] private CampFireConfig _campFireConfig;
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private CampFireHealth _healthFire;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private GameObject _restartUI;

    private void OnEnable()
    {
        _restartUI.SetActive(false);
        _health.OnDecrease += UpdateHealthBar;
        _healthFire.OnDecrease += UpdateCampFireHealthBar;
        _healthFire.OnIncrease += UpdateCampFireHealthBar;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        _health.OnDecrease -= UpdateHealthBar;
        _healthFire.OnDecrease -= UpdateCampFireHealthBar;
        _healthFire.OnIncrease -= UpdateCampFireHealthBar;
    }

    private void UpdateHealthBar(float value)
    {
        _healthUI.fillAmount = _health.Current / _config.MaxHP;
        if (_healthUI.fillAmount == _config.MinHP)
        {
            EndGame();
        }
    }

    private void UpdateCampFireHealthBar()
    {
        _campFireHealthUI.fillAmount = _healthFire.Current/ _campFireConfig.MaxHP;
        if (_campFireHealthUI.fillAmount == _campFireConfig.MinHP)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        _gameUI.LoseText();
        _restartUI.SetActive(true);
        Time.timeScale = 0;
    }
}
