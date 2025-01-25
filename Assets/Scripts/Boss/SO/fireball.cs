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
            Debug.LogWarning("으악 플레이어");
            StartCoroutine(PlayAndDeactivate());
        }
        else if (collision.CompareTag("Wall"))
        {
            Debug.LogWarning("으악 월");
            StartCoroutine(PlayAndDeactivate());
        }
    }

    private IEnumerator PlayAndDeactivate()
    {
        _rigid.velocity = Vector3.zero;
        // 애니메이션 트리거 실행
        _animator.SetTrigger("Explode");

        // 애니메이션의 길이만큼 대기
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        // 오브젝트 비활성화
        gameObject.SetActive(false);

        // 위치 초기화
        gameObject.transform.position = Vector3.zero;
    }
}