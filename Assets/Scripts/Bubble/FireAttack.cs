using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    private Coroutine _damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Boss"))
        {
            
            if (_damageCoroutine == null)
            {
                
                _damageCoroutine = StartCoroutine(DealDamageOverTime(collision));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamageOverTime(Collider2D collision)
    {
        int time = 0;
        while (time >= 3)
        {
            collision.GetComponent<BossHp>().OnDamage(0.5f);  // 1초에 1씩 데미지
            time += 1;
            yield return new WaitForSeconds(0.5f);  // 1초 대기
        }
        gameObject.SetActive(false);
    }
}