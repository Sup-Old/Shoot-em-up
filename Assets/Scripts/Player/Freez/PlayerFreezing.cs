using UnityEngine;
using UnityEngine.UI;

public class PlayerFreezing : MonoBehaviour
{
    [Header("Настройки замерзания")]
    [SerializeField] private float freezeDelay = 3f;
    [SerializeField] private float damagePerSecond = 5f;
    [SerializeField] private Image freezeOverlay;

    private PlayerFreezingZoneDetector _zoneDetector;
    private PlayerHealth _playerHealth;
    private float _timeOutside;

    private void Start()
    {
        _zoneDetector = GetComponent<PlayerFreezingZoneDetector>();
        _playerHealth = GetComponentInChildren<PlayerHealth>();

        if (freezeOverlay != null)
            freezeOverlay.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        if (!_zoneDetector.IsInLightZone)
        {
            _timeOutside += Time.deltaTime;

            float t = Mathf.Clamp01(_timeOutside / (freezeDelay * 10f)); // УМЕНЬШИЛИ СКОРОСТЬ В 10 раз
            if (freezeOverlay != null)
                freezeOverlay.color = new Color(1, 1, 1, t * 0.6f);

            if (_timeOutside >= freezeDelay)
            {
                _playerHealth.Decrease(damagePerSecond * Time.deltaTime);
            }
        }
        else
        {
            _timeOutside = 0f;
            if (freezeOverlay != null)
                freezeOverlay.color = new Color(1, 1, 1, 0);
        }
    }

}