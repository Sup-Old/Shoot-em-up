using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rangeWeaponPrefabs;
    [SerializeField] private List<RangeWeaponConfig> _rangeWeaponConfig;
    [SerializeField] private List<GameObject> _meleeWeaponPrefabs;
    [SerializeField] private List<MeleeWeaponConfig> _meleeWeaponConfig;

    [SerializeField] private List<SpriteArrayWrapper> _playerSkins;

    [SerializeField] private GameObject _upgradeWindow;
    [SerializeField] private DayNightManager _dayNightManager;

    [SerializeField] private List<UIUpgradeSlot> _upgradeSlots;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponSentry _weaponSentry;

    [SerializeField] private ExperienceUI _experienceUI;

    public void ShowUpgradeWindow()
    {
        if (_experienceUI.getExpBarsCount() >= 0)
        {
            _dayNightManager.ShowWindow();
            StartCoroutine(Show());
        }
    }

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(5);
        _dayNightManager.CloseWindow();
        if (_experienceUI.getExpBarsCount() > 0)
        {
            _upgradeWindow.SetActive(true);
            SpawnUpgradeOptions();
        }
    }

    private void CloseUpgradeWindow()
    {
        _upgradeWindow.SetActive(false);

        _experienceUI.DecreaseCount(1);
        if (_experienceUI.getExpBarsCount() > 0)
        {
            _upgradeWindow.SetActive(true);
            SpawnUpgradeOptions();
        }
    }

    private void SpawnUpgradeOptions()
    {
        foreach (var slot in _upgradeSlots)
        {
            foreach (Transform child in slot.SpawnPoint)
            {
                Destroy(child.gameObject);
            }
        }

        SpawnItem(_rangeWeaponPrefabs, _upgradeSlots[0], UpgradeRangeWeapon);
        SpawnItem(_meleeWeaponPrefabs, _upgradeSlots[1], UpgradeMeleeWeapon);
        SpawnSkin(_playerSkins, _upgradeSlots[2], ChangePlayerSprite);
    }

    private void SpawnItem(List<GameObject> itemList, UIUpgradeSlot slot, UnityAction clickAction)
    {
        if (itemList.Count == 0) return;

        var weaponPrefab = itemList[0];
        Sprite weaponSprite = weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;

        if (weaponSprite == null)
        {
            Debug.LogError("В префабе отсутствует спрайт в компоненте SpriteRenderer.");
            return;
        }

        Button btn = slot.button;
        if (btn == null)
        {
            Debug.LogError("В UIUpgradeSlot не назначена кнопка (button).");
            return;
        }

        Image img = slot.SpawnPoint.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("В кнопке отсутствует компонент Image.");
            return;
        }
        img.sprite = weaponSprite;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(clickAction);
    }

    private void SpawnSkin(List<SpriteArrayWrapper> skinList, UIUpgradeSlot slot, UnityAction clickAction)
    {
        if (skinList.Count == 0) return;

        Button btn = slot.button;
        if (btn == null)
        {
            Debug.LogError("В UIUpgradeSlot не назначена кнопка (button).");
            return;
        }

        Image img = slot.SpawnPoint.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("В кнопке отсутствует компонент Image.");
            return;
        }
        img.sprite = skinList[0].sprites[0];

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(clickAction);
    }

    public void ChangePlayerSprite()
    {
        if (_playerSkins.Count > 0)
        {
            _player.ChangeSprite(_playerSkins[0].sprites[0], _playerSkins[0].sprites[1]);
            _playerSkins.RemoveAt(0);
        }
        CloseUpgradeWindow();
    }


    public void UpgradeRangeWeapon()
    {
        if (_rangeWeaponPrefabs.Count > 0)
        {
            var newWeapon = _rangeWeaponPrefabs[0];
            
            _weaponSentry.ChangeWeapon(newWeapon, _rangeWeaponConfig[0]);
            _rangeWeaponPrefabs.RemoveAt(0);
            _rangeWeaponConfig.RemoveAt(0);
        }
        CloseUpgradeWindow();
    }

    public void UpgradeMeleeWeapon()
    {
        if (_meleeWeaponPrefabs.Count > 0)
        {
            var newWeapon = _meleeWeaponPrefabs[0];
            _weaponSentry.ChangeWeapon(newWeapon, _meleeWeaponConfig[0]);
            _meleeWeaponPrefabs.RemoveAt(0);
            _meleeWeaponConfig.RemoveAt(0);
        }
        CloseUpgradeWindow();
    }
}

[System.Serializable]
public class SpriteArrayWrapper
{
    public List<Sprite> sprites;
}
