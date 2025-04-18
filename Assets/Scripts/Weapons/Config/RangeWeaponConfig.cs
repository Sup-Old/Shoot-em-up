using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeaponConfig", menuName = "Config/Weapon/RangeWeapon", order = 51)]

public class RangeWeaponConfig : ScriptableObject
{
    [field: SerializeField] public float WeaponDamage { get; private set; } = 0;

    [field: SerializeField] public float BulletLifeTime { get; private set; } = 0f;

    [field: SerializeField] public float BulletSpeed { get; private set; } = 0f;
}
