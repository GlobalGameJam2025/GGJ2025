using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _highSpeed =20;
    private float _exp = 1;
    public float _currentSpeed;
    [HideInInspector]
    public Vector3 direction;
    [HideInInspector]
    public bool isfire;
    private float elapsedTime = 0f;

    public void Init()
    {
        _exp = 1;
        isfire = false;   
    }
   

    private void Update()
    {
        if (isfire)
        {
            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 지수적으로 감속 (elapsedTime을 사용)
            _currentSpeed = _highSpeed * Mathf.Exp(-3 * elapsedTime);
            //Debug.Log(_currentSpeed);

            // 속도가 0 이하로 떨어지면 멈춤
            if (_currentSpeed <= 0.1f)
            {
                _currentSpeed = 0f;
                Debug.Log("Projectile stopped.");
                return;
            }

            // 방향으로 이동
            this.transform.position += direction * _currentSpeed * Time.deltaTime;
        }
    }

}
