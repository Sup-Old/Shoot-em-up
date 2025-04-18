using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CampFireLightSystem : MonoBehaviour
{
    [Header("��������� ��������� �����")]
    [SerializeField] private Light2D _fireLight;

    // ������������ ������� (X=5, Y=3.5)
    [SerializeField] private Vector2 _maxScale = new Vector2(5f, 3.5f);

    // ����������� ������� (��������, X=1, Y=0.7)
    [SerializeField] private Vector2 _minScale = new Vector2(1f, 0.7f);

    [SerializeField] private float _minIntensity = 0.3f;
    [SerializeField] private float _maxIntensity = 1.5f;

    private CampFireHealth _fireHealth;

    private void Start()
    {
        _fireHealth = GetComponent<CampFireHealth>();

        if (_fireHealth == null || _fireLight == null)
        {
            Debug.LogError("�� ������� �����������!");
            enabled = false;
            return;
        }

        _fireHealth.OnHealthChanged += UpdateLight;
        UpdateLight(_fireHealth.GetHealthPercent());
    }

    private void UpdateLight(float healthPercent)
    {
        if (_fireLight == null) return;

        // ���������� ��������� �������� �� X � Y
        float scaleX = Mathf.Lerp(_minScale.x, _maxScale.x, healthPercent);
        float scaleY = Mathf.Lerp(_minScale.y, _maxScale.y, healthPercent);

        _fireLight.transform.localScale = new Vector3(scaleX, scaleY, 1f);

        // ������������� (�����������)
        _fireLight.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, healthPercent);

        //Debug.Log($"������: X={scaleX}, Y={scaleY}");
    }

    private void OnDestroy()
    {
        if (_fireHealth != null)
            _fireHealth.OnHealthChanged -= UpdateLight;
    }
}