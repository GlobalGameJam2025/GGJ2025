using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Boss/KnightAttackPattern2SO", fileName = "KnightAttackPattern2")]
public class KnightAttackPattern2SO : BossAttackPatternSO
{
    [SerializeField] private GameObject _fireBallPrefab;
    private List<GameObject> _fireBalls = new List<GameObject>();
    private float _attackSpeed = 15f;
    private float _attackDelayTime = 0.3f;

    public void Init()
    {
        _fireBalls.Clear();
        for (int i = 0; i < 3; i++)
        {
            GameObject fireBall = Instantiate(_fireBallPrefab);
            fireBall.SetActive(false);
            _fireBalls.Add(fireBall);
        }
    }
    public override IEnumerator Attack(GameObject player, GameObject boss)
    {
        Debug.Log("공격2");
        Vector2 directionToPlayer = (player.transform.position - boss.transform.position).normalized;

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(_attackDelayTime);
            directionToPlayer = (player.transform.position - boss.transform.position).normalized;

            // 비활성화된 파이어볼 찾기
            GameObject fireBall = _fireBalls.Find(fb => !fb.activeInHierarchy);

            if (fireBall != null)
            {
                fireBall.transform.position = boss.transform.position; // 보스 위치에서 발사
                fireBall.SetActive(true);
                fireBall.GetComponent<Rigidbody2D>().velocity = directionToPlayer * _attackSpeed;
            }
        }

        KnightBossController bossController = boss.GetComponent<KnightBossController>();
        yield return new WaitForSeconds(2f);
        bossController.stateMachine.TransitionTo(bossController.stateMachine.moveState);
    }
}