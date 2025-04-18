using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButon;

    private void Awake()
    {
        _playButton.onClick.AddListener(() => Play());
        _exitButon.onClick.AddListener(() => Quit());
    }

    private void Play()
    {
        SceneManager.LoadScene("Game");
    }

    private void Quit()
    {
        Application.Quit();
    }
}
