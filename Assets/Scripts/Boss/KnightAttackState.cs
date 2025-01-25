using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

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
        _controller.SetStop(true);
        _controller.animator.SetBool("Attack", true);
    }

    public void Exit()
    {
        isAttacking = false;
        _controller.animator.SetBool("Attack", false);
        _controller.SetStop(false);
    }

    public void Update()
    {
        if (_controller.onPatternNormal && _controller.onPatternNormalCoolTime && !isAttacking)
        {
            isAttacking = true;
            _controller.onPatternNormal = false;
            _controller.onPatternNormalCoolTime = false;
            _controller.StartCoroutine(_controller.patternNormal.Attack(_controller.player.gameObject, _controller.gameObject));
        }

        if (_controller.onPattern1 && _controller.onPattern1and2CoolTime && !isAttacking)
        {
            isAttacking = true;
            _controller.onPattern1 = false;
            _controller.onPattern1and2CoolTime = false;
            _controller.StartCoroutine(_controller.pattern1.Attack(_controller.player.gameObject, _controller.gameObject));
        }
        if(_controller.onPattern2 && _controller.onPattern1and2CoolTime && !isAttacking)
        {
            isAttacking = true;
            _controller.onPattern2 = false;
            _controller.onPattern1and2CoolTime = false;
            _controller.StartCoroutine(_controller.pattern2.Attack(_controller.player.gameObject, _controller.gameObject));
        }
        if(_controller.onPattern3 && _controller.onPattern3CoolTime && !isAttacking)
        {
            isAttacking = true;
            _controller.onPattern3 = false;
            _controller.onPattern3CoolTime = false;
            _controller.StartCoroutine(_controller.pattern3.Attack(_controller.player.gameObject, _controller.gameObject));
        }
    }
}