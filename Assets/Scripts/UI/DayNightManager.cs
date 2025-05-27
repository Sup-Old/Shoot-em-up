using System.Collections;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private GameObject _circle;


    [SerializeField] private float rotationDuration = 2f;

    private bool isRotating = false;

    public void ShowWindow()
    {
        _window.SetActive(true);
        RotateObject();
    }

    public void CloseWindow()
    {
        _window.SetActive(false);
    }

    public void RotateObject()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateByAngle(-180f, rotationDuration));
        }
    }

    private IEnumerator RotateByAngle(float angle, float duration)
    {
        isRotating = true;
        Quaternion startRotation = _circle.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 0, -angle);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            _circle.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _circle.transform.rotation = targetRotation;
        isRotating = false;
    }


}
