using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class Bubble : MonoBehaviour
{
    protected int _bombCount = 1;
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
        _bombCount = 1;
        gameObject.transform.localScale = Vector3.one;
    }

    private void Merge()
    {
        _bombCount++;
        gameObject.transform.localScale *= 1.2f;
    }

    protected abstract void Init();

    protected virtual void TriggerBubble()
    {
        if (_bombCount == 5)
        {
            gameObject.SetActive(false);
            return;
        }
        Merge();
    }

    protected virtual void TriggerPlayer()
    {
        if (!_isHead) return;
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
            if (priority > collision.GetComponent<Bubble>().priority)
            {
                gameObject.SetActive(false);
            }
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
