using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponConfig", menuName = "Config/Weapon/MeleeWeapon", order = 51)]
public class MeleeWeaponConfig : ScriptableObject
{
    [field: SerializeField] public float WeaponDamage { get; private set; } = 0;

    [field: SerializeField] public int AttackCoolDown { get; private set; } = 0;
}
