using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private Slider _expSlider;
    [SerializeField] public PlayerExperience _playerExperience;
    [SerializeField] private int _maxExp = 100;

    private int _fullExpBars = 0;

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
        if (_playerExperience.Current >= _maxExp)
        {
            _fullExpBars++; 
            _playerExperience.Decrease(_maxExp); 
        }

        Debug.Log(_playerExperience.Current);
        Debug.Log(_maxExp);

        _expSlider.value = (float)_playerExperience.Current / _maxExp;
    }

    public int getExpBarsCount() { return  _fullExpBars; }
    public void DecreaseCount(int decrease) { _fullExpBars -= decrease;}
}
