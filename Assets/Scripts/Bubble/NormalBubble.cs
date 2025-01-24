using UnityEngine;

public class NormalBubble : Bubble
{
    [SerializeField] private GameObject[] _childBubbles;
    [SerializeField] private float _launchForce = 5f; // �߻� �ӵ�

    protected override void TriggerBubble()
    {
        base.TriggerBubble();
        
        if (_bombCount == 5)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            // 8�������� �߻�
            for (int i = 0; i < 8; i++)
            {
                // ������ 45�� ������ ������ ���
                float angle = i * 45f;

                // ������ ���� �������� ��ȯ (2D ����)
                Vector2 direction = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),  // X ����
                    Mathf.Sin(angle * Mathf.Deg2Rad)   // Y ����
                );

                // �ڽ� ������ �ش� �������� �߻�
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