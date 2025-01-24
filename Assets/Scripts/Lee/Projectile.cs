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
            // ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            // ���������� ���� (elapsedTime�� ���)
            _currentSpeed = _highSpeed * Mathf.Exp(-3 * elapsedTime);
            //Debug.Log(_currentSpeed);

            // �ӵ��� 0 ���Ϸ� �������� ����
            if (_currentSpeed <= 0.1f)
            {
                _currentSpeed = 0f;
                Debug.Log("Projectile stopped.");
                return;
            }

            // �������� �̵�
            this.transform.position += direction * _currentSpeed * Time.deltaTime;
        }
    }

}
