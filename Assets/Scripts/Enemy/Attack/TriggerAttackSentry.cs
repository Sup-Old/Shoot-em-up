using System;
using UnityEngine;

public class TriggerAttackSentry : MonoBehaviour
{
    [SerializeField] private int _playerLayerID;

    public event Action OnZoneEnter;
    public event Action OnZoneExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != _playerLayerID)
        {
            return;
        }
        OnZoneEnter?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != _playerLayerID)
        {
            return;
        }
        OnZoneExit?.Invoke();
    }
}
