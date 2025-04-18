using UnityEngine;

public class WoodsEraser : MonoBehaviour
{
    [SerializeField] private int _campFireLayerID;
    [SerializeField] private float _destroyTimeDelay;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != _campFireLayerID)
        {
            return;
        }
        Destroy(gameObject,_destroyTimeDelay);
    }
}
