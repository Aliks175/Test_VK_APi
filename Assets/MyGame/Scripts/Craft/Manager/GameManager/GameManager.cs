using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AsyncOperation = UnityEngine.AsyncOperation;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _panelLoading;
    [SerializeField] private Slider _loadSlider;
    private List<AsyncOperation> asyncOperations = new List<AsyncOperation>();

    private void Awake()
    {
        instance = this;
        asyncOperations.Add(SceneManager.LoadSceneAsync((int)ListScene.Menu, LoadSceneMode.Additive));
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _panelLoading.SetActive(true);
        // asyncOperations.Add(  Инициализация
        // здесь мы должны взять сохранения игры из вк если они есть и инициировать в игре
        StartCoroutine(GetSceneLoadProgress(InitializeMenu));
    }

    public void LoadGame()
    {
        _panelLoading.SetActive(true);
        asyncOperations.Clear();
        asyncOperations.Add(SceneManager.UnloadSceneAsync((int)ListScene.Menu));
        asyncOperations.Add(SceneManager.LoadSceneAsync((int)ListScene.Game, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress(InitializeGame));
    }



    public void LoadMenu()
    {
        _panelLoading.SetActive(true);
        asyncOperations.Clear();
        asyncOperations.Add(SceneManager.UnloadSceneAsync((int)ListScene.Game));
        asyncOperations.Add(SceneManager.LoadSceneAsync((int)ListScene.Menu, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress(InitializeMenu));
    }

    private void InitializeGame()
    {
        SetUpGame setUpGame = GameObject.FindFirstObjectByType<SetUpGame>();
        setUpGame.Initialize();
    }

    private void InitializeMenu()
    {
        SetUpMenu setUpMenu = GameObject.FindFirstObjectByType<SetUpMenu>();
        setUpMenu.Initialize();
    }

    private IEnumerator GetSceneLoadProgress(Action action)
    {
        for (int i = 0; i < asyncOperations.Count; i++)
        {
            while (!asyncOperations[i].isDone)
            {
                yield return null;
            }
        }
        action?.Invoke();
        yield return new WaitForSeconds(0.2f);
        _panelLoading.SetActive(false);
    }
}

public enum ListScene
{
    Null = 0,
    Menu = 1,
    Game = 2,
}