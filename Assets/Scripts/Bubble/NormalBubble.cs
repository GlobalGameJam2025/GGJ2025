using System.Collections.Generic;
using UnityEngine;

public class NormalBubble : Bubble
{
    [SerializeField] private GameObject splashBubblePrefab; // 버블 프리팹
    [SerializeField] private int poolSize = 8;              // 풀 크기
    [SerializeField] private float _launchForce = 5f;       // 발사 속도

    private static List<GameObject> _sharedBubblePool;      // 공유 풀
    private static bool _isPoolInitialized = false;         // 풀 초기화 여부

    private void Awake()
    {
        // 공유 풀 초기화
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
            bubble.SetActive(false); // 비활성화 상태로 초기화
            _sharedBubblePool.Add(bubble); // 풀에 추가
        }
    }

    protected override void TriggerBubble()
    {
        base.TriggerBubble();

        if (_bombCount == 5)
        {
            // 자기 자신 비활성화
            gameObject.SetActive(false);
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            // 8방향으로 발사
            for (int i = 0; i < 8; i++)
            {
                GameObject bubble = GetBubbleFromSharedPool(); // 풀에서 버블 가져오기

                if (bubble != null)
                {
                    // 방향 계산 (45도 단위)
                    float angle = i * 45f;
                    Vector2 direction = new Vector2(
                        Mathf.Cos(angle * Mathf.Deg2Rad),
                        Mathf.Sin(angle * Mathf.Deg2Rad)
                    );

                    // 버블 초기화 및 발사
                    bubble.transform.position = transform.position; // 현재 위치에서 발사
                    bubble.SetActive(true); // 활성화
                    LaunchBubble(bubble, direction);
                }
            }
        }
    }

    private GameObject GetBubbleFromSharedPool()
    {
        // 풀에서 비활성화된 버블을 찾기
        foreach (GameObject bubble in _sharedBubblePool)
        {
            if (!bubble.activeInHierarchy)
            {
                return bubble; // 비활성화된 버블 반환
            }
        }

        Debug.LogWarning("활성화 가능한 버블이 없습니다!");
        return null; // 사용할 수 있는 버블 없음
    }

    private void LaunchBubble(GameObject bubble, Vector2 direction)
    {
        // Rigidbody2D를 사용해 속도를 설정
        Rigidbody2D rigid = bubble.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.velocity = direction * _launchForce;
        }
    }

    protected override void Init()
    {
        // 필요한 초기화 작업을 여기에 작성
    }
}
