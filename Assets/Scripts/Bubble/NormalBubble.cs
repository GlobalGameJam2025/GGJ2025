using System.Collections.Generic;
using UnityEngine;

public class NormalBubble : Bubble
{
    [SerializeField] private GameObject splashBubblePrefab;
    [SerializeField] private int poolSize = 8;
    [SerializeField] private float _launchForce = 5f;

    private static List<GameObject> _sharedBubblePool;
    private static bool _isPoolInitialized = false;

    private void Awake()
    {
        if (!_isPoolInitialized)
        {
            InitializeSharedPool();
            _isPoolInitialized = true;
        }
    }

    private void InitializeSharedPool()
    {
        _sharedBubblePool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bubble = Instantiate(splashBubblePrefab);
            bubble.SetActive(false);
            _sharedBubblePool.Add(bubble);
        }
    }

    protected override void TriggerBubble()
    {
        base.TriggerBubble();

        if (bombCount >= 5)
        {
            gameObject.SetActive(false);
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            // 8방향으로 발사
            for (int i = 0; i < 8; i++)
            {
                GameObject bubble = GetBubbleFromSharedPool();

                if (bubble != null)
                {
                    float angle = i * 45f;
                    Vector2 direction = new Vector2(
                        Mathf.Cos(angle * Mathf.Deg2Rad),
                        Mathf.Sin(angle * Mathf.Deg2Rad)
                    );

                    bubble.transform.position = transform.position;
                    bubble.SetActive(true);
                    LaunchBubble(bubble, direction);
                }
            }
        }
    }

    private GameObject GetBubbleFromSharedPool()
    {
        foreach (GameObject bubble in _sharedBubblePool)
        {
            if (!bubble.activeInHierarchy)
            {
                return bubble;
            }
        }

        Debug.LogWarning("활성화 가능한 버블이 없습니다!");
        return null;
    }

    private void LaunchBubble(GameObject bubble, Vector2 direction)
    {
        Rigidbody2D rigid = bubble.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.velocity = direction * _launchForce;
        }
    }
}
