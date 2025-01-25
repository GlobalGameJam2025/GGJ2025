using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class KnightBossController : MonoBehaviour
{
    private KnightBossStateMachine _stateMachine;

    public KnightBossStateMachine stateMachine
    {
        get { return _stateMachine; }
    }


    private bool _isRunning = true;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public Animator animator
    {
        get { return _animator; }
    }

    void Start()
    {
        _stateMachine = new KnightBossStateMachine(this);
        _stateMachine.Initialize(_stateMachine.moveState);
        agent.updateUpAxis = false; 
        agent.updateRotation = false; 
    }

    public void GoTarget()
    {
        if(_player.position.x < transform.position.x)
        {
            _spriteRenderer.flipX = false;   
        }
        else
        {
            _spriteRenderer.flipX = true;   
        }
        agent.SetDestination(_player.position);
    }

    public float GetDistance()
    {
        return  Vector3.Distance(transform.position, _player.position);
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