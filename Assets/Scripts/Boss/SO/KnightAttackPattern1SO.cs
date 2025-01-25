using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/KnightAttackPattern1SO", fileName = "KnightAttackPattern1")]
public class KnightAttackPattern1SO : BossAttackPatternSO
{
    public override IEnumerator Attack(GameObject player, GameObject boss)
    {
        Debug.Log("АјАн1");
        KnightBossController bossController = boss.GetComponent<KnightBossController>();
        yield return new WaitForSeconds(2f);
        bossController.stateMachine.TransitionTo(bossController.stateMachine.moveState);
    }
}