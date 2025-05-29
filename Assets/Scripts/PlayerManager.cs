using Cinemachine;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private GameObject playerInstance;
    private GameObject upgradeManager;
    private GameObject canvas;
    private GameObject dayNightManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            playerInstance = GameObject.FindGameObjectWithTag("Player");
            DontDestroyOnLoad(playerInstance);

            upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager");
            DontDestroyOnLoad(upgradeManager);

            canvas = GameObject.FindGameObjectWithTag("Canvas");
            DontDestroyOnLoad(canvas);

            dayNightManager = GameObject.FindGameObjectWithTag("DayNightManager");
            DontDestroyOnLoad(dayNightManager);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerInstance != null)
        {
            SceneManager.MoveGameObjectToScene(playerInstance, scene);
            SceneManager.MoveGameObjectToScene(canvas, scene);
            SceneManager.MoveGameObjectToScene(dayNightManager, scene);
            Debug.Log("PlayerManager: Игрок перемещён в сцену " + scene.name);
            dayNightManager.GetComponent<DayNightManager>().SetCampFire(GameObject.FindGameObjectWithTag("CampFire"));
            GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineVirtualCamera>().Follow = playerInstance.transform;
            SceneManager.MoveGameObjectToScene(upgradeManager, scene);
        }
    }

    /// <summary>
    /// Метод для сброса (удаления) persistent объектов, вызывается при перезапуске игры.
    /// </summary>
    public void ResetGame()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = null;
        }
        if (upgradeManager != null)
        {
            Destroy(upgradeManager);
            upgradeManager = null;
        }
        if (canvas != null)
        {
            Destroy(canvas);
            canvas = null;
        }
        if (dayNightManager != null)
        {
            Destroy(dayNightManager);
            dayNightManager = null;
        }
        Destroy(gameObject);
        Instance = null;
    }
}
