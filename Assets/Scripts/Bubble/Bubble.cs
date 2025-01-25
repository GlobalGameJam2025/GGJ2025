using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class Bubble : MonoBehaviour
{
    public int bombCount = 1;
    [SerializeField] protected bool _isHead = true;
    public static int count = 0;
    public int priority = 0;
    private Rigidbody2D _rigidbody;
    private float _speedThreshold = 0.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        priority = count;
        count++;
    }

    private void OnDisable()
    {
        bombCount = 1;
        gameObject.transform.localScale = Vector3.one;
    }

    protected virtual void TriggerBubble()
    {
        if (bombCount >= 5) return;
        bombCount++;
        gameObject.transform.localScale *= 1.2f;
    }

    protected virtual void TriggerPlayer()
    {
        if (_isHead) return;
        gameObject.SetActive(false);
    }

    protected virtual void TriggerBoss()
    {
        gameObject.SetActive(false);
    }

    protected virtual void SetExplosion(bool isExplosion = true)
    {
        if (isExplosion) return;
        if (bombCount >= 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            if (priority > collision.GetComponent<Bubble>().priority)
            {
                gameObject.SetActive(false);
            }
            TriggerBubble();
            SetExplosion();
            
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
