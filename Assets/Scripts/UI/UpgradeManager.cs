using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rangeWeaponPrefabs;
    [SerializeField] private List<GameObject> _meleeWeaponPrefabs;
    [SerializeField] private List<Sprite> _playerSkins;

    [SerializeField] private GameObject _upgradeWindow;
    [SerializeField] private List<UIUpgradeSlot> _upgradeSlots;
    [SerializeField] private Player _player;

    public void ShowUpgradeWindow()
    {
        _upgradeWindow.SetActive(true);
        SpawnUpgradeOptions();
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

        SpawnItem(_rangeWeaponPrefabs, _upgradeSlots[0]);
        SpawnItem(_meleeWeaponPrefabs, _upgradeSlots[1]);
        SpawnSkin(_playerSkins, _upgradeSlots[2]);
    }

    private void SpawnItem(List<GameObject> itemList, UIUpgradeSlot slot)
    {
        if (itemList.Count == 0) return;

        var prefab = itemList[0];
        Instantiate(prefab, slot.SpawnPoint);
    }

    private void SpawnSkin(List<Sprite> skinList, UIUpgradeSlot slot)
    {
        if (skinList.Count == 0) return;

        var spriteRenderer = slot.SpawnPoint.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = skinList[0];
        }
    }

    public void ChangePlayerSprite()
    {
        if (_playerSkins.Count > 0)
        {
            _player.ChangeSprite();
            _playerSkins.RemoveAt(0);
        }
        CloseUpgradeWindow();
    }

    private void CloseUpgradeWindow()
    {
        _upgradeWindow.SetActive(false);
    }

    public void UpgradeRangeWeapon()
    {
        if (_rangeWeaponPrefabs.Count > 0)
        {
            var newWeapon = _rangeWeaponPrefabs[0];
            _player.GetComponent<WeaponSentry>().ChangeWeapon(newWeapon, false);
            _rangeWeaponPrefabs.RemoveAt(0);
        }
        CloseUpgradeWindow();
    }

    public void UpgradeMeleeWeapon()
    {
        if (_meleeWeaponPrefabs.Count > 0)
        {
            var newWeapon = _meleeWeaponPrefabs[0];
            _player.GetComponent<WeaponSentry>().ChangeWeapon(newWeapon, true);
            _meleeWeaponPrefabs.RemoveAt(0);
        }
        CloseUpgradeWindow();
    }


}
