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
            // ��ǥ ���� ���
            Vector3 direction = (_target.position - transform.position).normalized;

            // ��ǥ �������� �̵�
            transform.position += direction * _projectileSpeed * Time.deltaTime;

            // ź���� ��ǥ�� �ٶ󺸵��� ȸ��
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
