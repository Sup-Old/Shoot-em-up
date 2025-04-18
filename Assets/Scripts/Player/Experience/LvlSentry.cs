using UnityEngine;

public class LvlCentry : MonoBehaviour
{
    [SerializeField] private PlayerExperience _playerExperience;
    [SerializeField] private int _lastExp;

    private void OnEnable()
    {
        _playerExperience.OnIncrease += LvlCheck;
    }

    private void OnDisable()
    {
        _playerExperience.OnIncrease -= LvlCheck;
    }

    private void LvlCheck()
    {
        if (_playerExperience.Current > _lastExp)
        {
            _lastExp = _playerExperience.Current;
            //...
        }
    }
}
