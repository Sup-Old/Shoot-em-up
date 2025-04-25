using UnityEngine;

public class PlayerFreezingZoneDetector : MonoBehaviour
{
    private bool _isInLightZone = false;

    public bool IsInLightZone => _isInLightZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LightZone"))
        {
            _isInLightZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LightZone"))
        {
            _isInLightZone = false;
        }
    }
}
