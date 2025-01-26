using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scull : MonoBehaviour
{
    public float damage;
    public void  SetFalse()
    {
        gameObject.SetActive(false);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SetHp(damage);
        }

    }

}
