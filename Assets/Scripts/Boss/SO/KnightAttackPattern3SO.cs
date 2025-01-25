using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/KnightAttackPattern3SO", fileName = "KnightAttackPattern3")]
public class KnightAttackPattern3SO : BossAttackPatternSO
{
    public override IEnumerator Attack(GameObject player, GameObject boss)
    {
        Debug.Log("АјАн3");
        KnightBossController bossController = boss.GetComponent<KnightBossController>();
        yield return new WaitForSeconds(2f);
        bossController.stateMachine.TransitionTo(bossController.stateMachine.moveState);
    }
}