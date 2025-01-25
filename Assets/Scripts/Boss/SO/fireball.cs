using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().SetHp(0.1f);
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            gameObject.transform.position = Vector3.zero;
        }
    }
}