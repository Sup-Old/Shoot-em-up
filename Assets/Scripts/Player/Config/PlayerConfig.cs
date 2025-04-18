using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/Player", order = 51)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float MaxHP { get; private set; }
    [field: SerializeField] public float MinHP { get; private set; }

}
