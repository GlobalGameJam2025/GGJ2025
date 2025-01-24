using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class LobbyHandler : MonoBehaviour
{
    [SerializeField] private Button _newStartBtn;
    [SerializeField] private Button _closeBtn;

    [SerializeField] private string _gameSceneName;

    private void Start()
    {
        _newStartBtn.onClick.AddListener(OnNewStart);

        _closeBtn.onClick.AddListener(OnExit);
    }

    private void OnNewStart()
    {
        SceneLoader.instance.LoadScene(_gameSceneName);
    }

    public void OnExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit(); // 어플리케이션 종료
        #endif
    }


}