using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBossController : MonoBehaviour
{
    private KnightBossStateMachine _stateMachine;
    private bool _isRunning = true;

    void Start()
    {
        _stateMachine = new KnightBossStateMachine(this);
        _stateMachine.Initialize(_stateMachine.idleState);
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (!_isRunning) return;
        _stateMachine.Update();
    }
}