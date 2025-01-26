using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(menuName = "Boss/KnightAttackPattern3SO", fileName = "KnightAttackPattern3")]
public class KnightAttackPattern3SO : BossAttackPatternSO
{
    public override IEnumerator Attack(GameObject player, GameObject boss)
    {
        Debug.Log("공격3");
        KnightBossController bossController = boss.GetComponent<KnightBossController>();

        // 현재 애니메이터 상태 가져오기
        AnimatorStateInfo stateInfo = bossController.animator.GetCurrentAnimatorStateInfo(0);

        // 현재 스테이트의 이름 가져오기
        string currentStateName = bossController.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        // 로그 출력
        Debug.Log($"Current Animation State: {currentStateName}");


        // 현재 애니메이션이 끝났는지 확인
        //if (stateInfo.normalizedTime >= 1f)
        //{
        //    bossController.animator.SetBool("Wrap", false);
        //}

        bossController.transform.position = bossController.bossTransform[0].position;
        yield return new WaitForSeconds(0.5f);
        bossController.transform.position = bossController.bossTransform[1].position;
        yield return new WaitForSeconds(0.5f);
        bossController.transform.position = bossController.bossTransform[2].position;
        yield return new WaitForSeconds(0.5f);
        bossController.transform.position = bossController.player.transform.position;

        float magnitude = (player.transform.position - boss.transform.position).magnitude;
        Debug.Log("기본 공격");
       

        yield return new WaitForSeconds(.7f);

        if (magnitude <= attackArea)
        {
            bossController.animator.Play("Attack");
            player.GetComponent<PlayerController>().SetHp(0.1f);
        }

        yield return new WaitForSeconds(2f);
        bossController.stateMachine.TransitionTo(bossController.stateMachine.moveState);
    }
}