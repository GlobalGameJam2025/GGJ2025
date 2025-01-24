using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bubble : MonoBehaviour
{
    protected int _bombCount = 1;
    [SerializeField] protected bool _isHead = true;

    private void Merge()
    {
        _bombCount++;
        gameObject.transform.localScale *= 1.2f;
    }

    protected virtual void TriggerBubble()
    {
        if(_bombCount > 5)
        {
            //gameObject.SetActive(false);
            return;
        }
        Merge();
        if (!_isHead)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void TriggerPlayer()
    {
        gameObject.SetActive(false);
    }

    protected virtual void TriggerBoss()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Bubble"))
        {
            TriggerBubble();
        }

        if (collision.CompareTag("Player"))
        {
            TriggerPlayer();
        }

        if (collision.CompareTag("Boss"))
        {
            TriggerBoss();
        }
    }
}