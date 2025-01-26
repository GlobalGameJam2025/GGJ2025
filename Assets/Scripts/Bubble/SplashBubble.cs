using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBubble : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
            collision.GetComponent<BossHp>().OnDamage(3f);
        }
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            gameObject.transform.position = Vector3.zero;
        }
    }
}