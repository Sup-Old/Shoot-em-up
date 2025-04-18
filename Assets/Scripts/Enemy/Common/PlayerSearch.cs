using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    [SerializeField] private string _tag;

    public GameObject ObjectToFollow { get; private set; }
    public Vector3 MoveDirection { get; private set; }

    private void OnEnable()
    {
        ObjectToFollow = GameObject.FindGameObjectWithTag(_tag);
    }

    private void Update()
    {
        MoveDirection = Vector3.Normalize(ObjectToFollow.transform.position - transform.position);
    }
}
