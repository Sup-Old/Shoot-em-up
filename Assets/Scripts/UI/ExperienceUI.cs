using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private Slider _expSlider;
    [SerializeField] private PlayerExperience _playerExperience;
    [SerializeField] private int _maxExp = 100;

    private void OnEnable()
    {
        _playerExperience.OnIncrease += UpdateSlider;
    }

    private void OnDisable()
    {
        _playerExperience.OnIncrease -= UpdateSlider;
    }

    private void UpdateSlider()
    {
        _expSlider.value = (float)_playerExperience.Current / _maxExp;
    }
}
