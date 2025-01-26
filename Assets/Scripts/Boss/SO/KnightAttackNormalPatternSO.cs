using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/KnightAttackNormalPatternSO", fileName = "KnightAttackNormalPattern")]
public class KnightAttackNormalPatternSO : BossAttackPatternSO
{

    public override IEnumerator Attack(GameObject player, GameObject boss)
    {
        float magnitude = (player.transform.position - boss.transform.position).magnitude;
        Debug.Log("기본 공격");

        yield return new WaitForSeconds(.7f);

        if (magnitude <= attackArea)
        {
            player.GetComponent<PlayerController>().SetHp(0.1f);
        }
        KnightBossController bossController = boss.GetComponent<KnightBossController>();
        yield return new WaitForSeconds(2f);
        bossController.stateMachine.TransitionTo(bossController.stateMachine.moveState);
    }
}