using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/Enemy", order = 51)]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public float MaxHealthPoints { get; private set; } = 0;

    [field: SerializeField] public float MinHealthPoints { get; private set; } = 0;

    [field: SerializeField] public float SpawnTime { get; private set; } = 0f;

    [field: SerializeField] public float MoveSpeed { get; private set; } = 0f;

    [field: SerializeField] public float Damage { get; private set; } = 0;

    [field: SerializeField] public float AttackCoolDown { get; private set; } = 2f;
}
