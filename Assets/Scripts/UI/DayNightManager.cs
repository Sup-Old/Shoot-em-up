using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _campFire;
    [SerializeField] private GameObject _text;


    [SerializeField] private float rotationDuration = 2f;

    private bool isRotating = false;

    public void ShowWindow()
    {
        _window.SetActive(true);
        setLight();
        RotateObject();
        
    }

    private void setLight()
    {
        _campFire.GetComponent<CampFire>().enabled = false;
        _campFire.GetComponent<CampFireHealth>().enabled = false;
        _campFire.GetComponentInChildren<Light2D>().shadowsEnabled = false;
        _campFire.GetComponent<CampFireLightSystem>().enabled = false;
        _campFire.GetComponentInChildren<Light2D>().transform.localScale = Vector3.one * 50;
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

    public void SetCampFire(GameObject CampFire) { _campFire = CampFire; }

    public void setText(bool b) { _text.SetActive(b); }
}
