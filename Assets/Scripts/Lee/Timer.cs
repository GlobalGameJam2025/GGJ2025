using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime = 300f; // 타이머 총 시간 (초 단위)
    public TextMeshProUGUI timerText; // 타이머를 표시할 TextMeshProUGUI 컴포넌트

    private float timeRemaining;
    private bool isTimerRunning = true;

    void Start()
    {
        // 타이머 초기화
        timeRemaining = totalTime;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                // 타이머 종료
                timeRemaining = 0;
                isTimerRunning = false;
                UpdateTimerText();
            }
        }
    }

    void UpdateTimerText()
    {
        // 분과 초 계산
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // 텍스트 갱신
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
