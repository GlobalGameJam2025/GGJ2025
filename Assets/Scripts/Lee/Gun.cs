using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float _projectileSpeed = 20;
    [SerializeField]
    private float damage;
    [SerializeField]
    private Transform _target;
    private bool _isfire;

    public void Init(Vector3 InitPos)
    {
        _isfire = true;
        gameObject.SetActive(true);
        transform.position = InitPos;
    }


    private void Update()
    {
        if (_isfire)
        {
            // 목표 방향 계산
            Vector3 direction = (_target.position - transform.position).normalized;

            // 목표 방향으로 이동
            transform.position += direction * _projectileSpeed * Time.deltaTime;

            // 탄알이 목표를 바라보도록 회전
            //transform.rotation = Quaternion.LookRotation(direction);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Player"))
        {
        }
        else
        {
            gameObject.SetActive(false);    
        }

    }

}
