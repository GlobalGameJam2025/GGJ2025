using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class KnightAttackState : IBossState
{
    private KnightBossController _controller;
    private bool isAttacking = false;

    public KnightAttackState(KnightBossController controller)
    {
        _controller = controller;
    }

    public void Enter()
    {
        _controller.animator.SetBool("Attack", true);
    }

    public void Exit()
    {

    }

    public void Update()
    {
       // if (_controller.onPatternNormal)
    }
}
