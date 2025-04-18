using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _loseText;
    private float _startTime;

    void Start()
    {
        _restartButton.onClick.AddListener(() => Restart());
        _exitButton.onClick.AddListener(() => Quit());
        _startTime = Time.time;
    }

    void Update()
    {
        var t = Time.time - _startTime;

        var minutes = ((int)t / 60).ToString();
        var seconds = ((int)t % 60).ToString();

        _timerText.text = minutes + ":" + seconds;
    }

    public void LoseText()
    {
        _loseText.text = "You Lose, result: " + _timerText.text;
    }

    private void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    private void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
