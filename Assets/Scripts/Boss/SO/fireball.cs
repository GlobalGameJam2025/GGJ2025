using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigid;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().SetHp(0.1f);
            Debug.LogWarning("���� �÷��̾�");
            StartCoroutine(PlayAndDeactivate());
        }
        else if (collision.CompareTag("Wall"))
        {
            Debug.LogWarning("���� ��");
            StartCoroutine(PlayAndDeactivate());
        }
    }

    private IEnumerator PlayAndDeactivate()
    {
        _rigid.velocity = Vector3.zero;
        // �ִϸ��̼� Ʈ���� ����
        _animator.SetTrigger("Explode");

        // �ִϸ��̼��� ���̸�ŭ ���
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        // ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);

        // ��ġ �ʱ�ȭ
        gameObject.transform.position = Vector3.zero;
    }
}