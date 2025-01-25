using System.Collections.Generic;
using UnityEngine;

public class NormalBubble : Bubble
{
    [SerializeField] private GameObject splashBubblePrefab; // ���� ������
    [SerializeField] private int poolSize = 8;              // Ǯ ũ��
    [SerializeField] private float _launchForce = 5f;       // �߻� �ӵ�

    private static List<GameObject> _sharedBubblePool;      // ���� Ǯ
    private static bool _isPoolInitialized = false;         // Ǯ �ʱ�ȭ ����

    private void Awake()
    {
        // ���� Ǯ �ʱ�ȭ
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
            bubble.SetActive(false); // ��Ȱ��ȭ ���·� �ʱ�ȭ
            _sharedBubblePool.Add(bubble); // Ǯ�� �߰�
        }
    }

    protected override void TriggerBubble()
    {
        base.TriggerBubble();

        if (_bombCount == 5)
        {
            // �ڱ� �ڽ� ��Ȱ��ȭ
            gameObject.SetActive(false);
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            // 8�������� �߻�
            for (int i = 0; i < 8; i++)
            {
                GameObject bubble = GetBubbleFromSharedPool(); // Ǯ���� ���� ��������

                if (bubble != null)
                {
                    // ���� ��� (45�� ����)
                    float angle = i * 45f;
                    Vector2 direction = new Vector2(
                        Mathf.Cos(angle * Mathf.Deg2Rad),
                        Mathf.Sin(angle * Mathf.Deg2Rad)
                    );

                    // ���� �ʱ�ȭ �� �߻�
                    bubble.transform.position = transform.position; // ���� ��ġ���� �߻�
                    bubble.SetActive(true); // Ȱ��ȭ
                    LaunchBubble(bubble, direction);
                }
            }
        }
    }

    private GameObject GetBubbleFromSharedPool()
    {
        // Ǯ���� ��Ȱ��ȭ�� ������ ã��
        foreach (GameObject bubble in _sharedBubblePool)
        {
            if (!bubble.activeInHierarchy)
            {
                return bubble; // ��Ȱ��ȭ�� ���� ��ȯ
            }
        }

        Debug.LogWarning("Ȱ��ȭ ������ ������ �����ϴ�!");
        return null; // ����� �� �ִ� ���� ����
    }

    private void LaunchBubble(GameObject bubble, Vector2 direction)
    {
        // Rigidbody2D�� ����� �ӵ��� ����
        Rigidbody2D rigid = bubble.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.velocity = direction * _launchForce;
        }
    }

    protected override void Init()
    {
        // �ʿ��� �ʱ�ȭ �۾��� ���⿡ �ۼ�
    }
}
