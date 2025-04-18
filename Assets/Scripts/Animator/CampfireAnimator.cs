using UnityEngine;

// Добавьте этот класс в отдельный файл или в начало вашего скрипта
public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }
}

public class CampfireAnimator : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private Animator _animator;
    [SerializeField] private CampFireHealth _fireHealth;
    [SerializeField] private float _blendSharpness = 5f;

    private const string HEALTH_PARAMETER = "HealthPercent";
    private float _currentBlend;
    private int _lastPhase;

    private void Awake()
    {
        Debug.Log("=== CampfireAnimator.Awake() ===");

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
            Debug.Log("Animator был null - назначен через GetComponent");
        }

        if (_fireHealth == null)
        {
            _fireHealth = GetComponentInParent<CampFireHealth>();
            Debug.Log("CampFireHealth был null - назначен через GetComponentInParent");
        }

        _animator.updateMode = AnimatorUpdateMode.Normal;
        _animator.applyRootMotion = false;
    }

    private void Start()
    {
        Debug.Log("=== CampfireAnimator.Start() ===");

        // Альтернативная проверка параметров без метода расширения
        bool hasParam = false;
        foreach (AnimatorControllerParameter param in _animator.parameters)
        {
            if (param.name == HEALTH_PARAMETER)
            {
                hasParam = true;
                break;
            }
        }

        if (!hasParam)
        {
            Debug.LogError($"Параметр '{HEALTH_PARAMETER}' не найден в аниматоре!");
            return;
        }

        Debug.Log("Проверка аниматора: OK");
    }

    private void OnEnable()
    {
        if (_fireHealth != null)
        {
            _fireHealth.OnHealthChanged += HandleHealthChanged;
            Debug.Log("Успешно подписались на OnHealthChanged");
            HandleHealthChanged(_fireHealth.GetHealthPercent());
        }
        else
        {
            Debug.LogError("Ошибка: CampFireHealth не найден!");
        }
    }

    private void OnDisable()
    {
        if (_fireHealth != null)
        {
            _fireHealth.OnHealthChanged -= HandleHealthChanged;
            Debug.Log("Отписались от OnHealthChanged");
        }
    }

    private void HandleHealthChanged(float healthPercent)
    {
        if (_animator == null)
        {
            Debug.LogError("Аниматор не назначен!");
            return;
        }

        float blendValue = 1f - Mathf.Clamp01(healthPercent);
        _currentBlend = Mathf.Lerp(_currentBlend, blendValue, _blendSharpness * Time.deltaTime);

        _animator.SetFloat(HEALTH_PARAMETER, _currentBlend);

        int currentPhase = Mathf.FloorToInt(_currentBlend * 9) + 1;
        currentPhase = Mathf.Clamp(currentPhase, 1, 9);

        Debug.Log($"Health: {healthPercent:F2} | Blend: {_currentBlend:F2} | Phase: {currentPhase}");

        if (currentPhase != _lastPhase)
        {
            _lastPhase = currentPhase;
            Debug.Log($"Смена фазы на: {currentPhase}");
        }
    }

    // Тестовое управление с клавиатуры
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float testValue = Random.Range(0f, 1f);
            _animator.SetFloat(HEALTH_PARAMETER, testValue);
            Debug.Log($"Тест: ручная установка {testValue:F2}");
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
        if (_fireHealth == null) _fireHealth = GetComponentInParent<CampFireHealth>();
    }
#endif
}