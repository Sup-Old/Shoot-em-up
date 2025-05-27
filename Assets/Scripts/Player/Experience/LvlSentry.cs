using System.Collections;
using UnityEngine;

public class LvlCentry : MonoBehaviour
{   
    [SerializeField] private PlayerExperience _playerExperience;
    [SerializeField] private int _lastExp;
    [SerializeField] private UpgradeManager _upgradeManager;

    private void OnEnable()
    {
        _playerExperience.OnIncrease += LvlCheck;
        StartCoroutine(LevelUpTimer());
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

    private IEnumerator LevelUpTimer()
    {
        yield return new WaitForSeconds(10);
        _upgradeManager.ShowUpgradeWindow();
    }
}
