using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRestart : MonoBehaviour
{
    [SerializeField] private Button _closeBtn;

    [SerializeField] private string _gameSceneName = "Lobby";

    private void Start()
    {
        _closeBtn.onClick.AddListener(OnExit);
    }

    public void OnExit()
    {
        SceneLoader.instance.LoadScene(_gameSceneName);
    }
}
