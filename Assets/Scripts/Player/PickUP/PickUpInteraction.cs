using UnityEngine;

public class PickUpInteraction : MonoBehaviour
{
    [SerializeField] private int _pickUpLayerID;
    [SerializeField] private Transform _transform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != _pickUpLayerID)
        {
            return;
        }
        collision.transform.SetParent(_transform);
    }
}
