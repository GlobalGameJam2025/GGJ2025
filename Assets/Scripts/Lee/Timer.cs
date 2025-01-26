using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime = 300f; // Ÿ�̸� �� �ð� (�� ����)
    public TextMeshProUGUI timerText; // Ÿ�̸Ӹ� ǥ���� TextMeshProUGUI ������Ʈ

    private float timeRemaining;
    private bool isTimerRunning = true;

    void Start()
    {
        // Ÿ�̸� �ʱ�ȭ
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
                // Ÿ�̸� ����
                timeRemaining = 0;
                isTimerRunning = false;
                UpdateTimerText();
            }
        }
    }

    void UpdateTimerText()
    {
        // �а� �� ���
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // �ؽ�Ʈ ����
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
