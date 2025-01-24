using UnityEngine;

public class NormalBubble : Bubble
{
    [SerializeField] private GameObject[] _childBubbles;
    [SerializeField] private float _launchForce = 5f; // 발사 속도

    protected override void TriggerBubble()
    {
        base.TriggerBubble();
        
        if (_bombCount == 5)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            // 8방향으로 발사
            for (int i = 0; i < 8; i++)
            {
                // 각도를 45도 단위로 나누어 계산
                float angle = i * 45f;

                // 각도를 벡터 방향으로 변환 (2D 기준)
                Vector2 direction = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),  // X 방향
                    Mathf.Sin(angle * Mathf.Deg2Rad)   // Y 방향
                );

                // 자식 버블을 해당 방향으로 발사
                _childBubbles[i].gameObject.SetActive(true);
                LaunchBubble(_childBubbles[i], direction);
            }
        }
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