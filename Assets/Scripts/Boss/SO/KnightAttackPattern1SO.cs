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
        bossController = boss.GetComponent<KnightBossController>();
        bossController.scullBooms[0].gameObject.SetActive(true);
        bossController.scullBooms[0].transform.position = bossController.scullBoomLocate[0].position;

        yield return new WaitForSeconds(0.3f);

        bossController.scullBooms[1].gameObject.SetActive(true);
        bossController.scullBooms[1].transform.position = bossController.scullBoomLocate[1].position;

        yield return new WaitForSeconds(0.3f);

        bossController.scullBooms[2].gameObject.SetActive(true);
        bossController.scullBooms[2].transform.position = bossController.scullBoomLocate[2].position;

        yield return new WaitForSeconds(0.3f);

        bossController.scullBooms[3].gameObject.SetActive(true);
        bossController.scullBooms[3].transform.position = bossController.scullBoomLocate[3].position;

        yield return new WaitForSeconds(2f);
        bossController.stateMachine.TransitionTo(bossController.stateMachine.moveState);


        
    }
}