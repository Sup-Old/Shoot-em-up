using UnityEngine;

public class CampFire : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private CampFireConfig _config;
    [SerializeField] private CampFireHealth _health;
    [SerializeField] private int _woodsLayerID;

    private void Awake()
    {
        Debug.Log("[CampFire] Awake() ����� ����������");

        if (_config == null)
            Debug.LogError("Config �� ��������!");

        if (_health == null)
        {
            _health = GetComponent<CampFireHealth>();
            Debug.Log("[CampFire] Health ������� ����� GetComponent");
        }

        Debug.Log($"[CampFire] ������������� ���������. Health: {_health != null}, Config: {_config != null}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[CampFire] OnTriggerEnter2D � {collision.gameObject.name} (layer: {collision.gameObject.layer})");

        if (collision.gameObject.layer != _woodsLayerID)
        {
            Debug.Log($"[CampFire] ������ {collision.name} �� �������� ������� (����� ���� {_woodsLayerID})");
            return;
        }

        Debug.Log($"[CampFire] ��������� ��������: +{_config.IncreaseHPValue}");
        _health.Increase(_config.IncreaseHPValue);
    }

    private void Update()
    {
        if (_config == null || _health == null)
        {
            Debug.LogWarning("[CampFire] Config ��� Health �� ���������!");
            return;
        }

        float healthBefore = _health.Current;
        _health.Decrease(_config.DecreaseHPValue);

        //Debug.Log($"[CampFire] Update: ��������� �������� � {healthBefore} �� {_health.Current} (-{_config.DecreaseHPValue})");
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Debug.Log("[CampFire] OnValidate()");
        if (_health == null) _health = GetComponent<CampFireHealth>();
    }
#endif
}