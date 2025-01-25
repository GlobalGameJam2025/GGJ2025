using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/KnightAttackNormalPatternSO", fileName = "KnightAttackNormalPattern")]
public class KnightAttackNormalPatternSO : BossAttackPatternSO
{
    public override IEnumerator Attack(GameObject player, GameObject boss)
    {
        float magnitude = (player.transform.position - boss.transform.position).magnitude;
        Debug.Log("플레이어와의 거리: "+magnitude);

        yield return new WaitForSeconds(1f);

        if (magnitude <= attackArea)
        {
            player.GetComponent<PlayerController>().SetHp(0.1f);
        }
    }
}