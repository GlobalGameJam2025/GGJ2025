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

    [SerializeField] private KnightAttackNormalPatternSO _patternNormal;
    [SerializeField] private KnightAttackPattern1SO _pattern1;
    [SerializeField] private KnightAttackPattern2SO _pattern2;
    [SerializeField] private KnightAttackPattern3SO _pattern3;
    public KnightAttackNormalPatternSO patternNormal => _patternNormal;
    public KnightAttackPattern1SO pattern1 => _pattern1;
    public KnightAttackPattern2SO pattern2 => _pattern2;
    public KnightAttackPattern3SO pattern3 => _pattern3;

    public PolygonCollider2D normalAttackArea;
    public GameObject[] scullBooms;
    public Transform[] scullBoomLocate;
    public Transform[] bossTransform;

    private bool _isRunning = true;
    [SerializeField]
    private PlayerController _player;
    public PlayerController player => _player;
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

    public bool onPatternNormal = false;
    public bool onPattern1 = false;
    public bool onPattern2 = false;
    public bool onPattern3 = false;

    public bool onPatternNormalCoolTime = true;
    public bool onPattern1and2CoolTime = true;
    public bool onPattern3CoolTime = true;

    [SerializeField] private float patternNormalCoolTime = 0;
    [SerializeField] private float pattern1and2CoolTime = 0;
    [SerializeField] private float pattern3CoolTime = 0;

    void Start()
    {
        _pattern2.Init();
        _stateMachine = new KnightBossStateMachine(this);
        _stateMachine.Initialize(_stateMachine.idleState);
        agent.updateUpAxis = false; 
        agent.updateRotation = false; 
    }

    public void GoTarget()
    {
        if(_player.gameObject.transform.position.x < transform.position.x)
        {
            _spriteRenderer.flipX = false;   
        }
        else
        {
            _spriteRenderer.flipX = true;   
        }
        agent.SetDestination(_player.gameObject.transform.position);
    }

    public float GetDistance()
    {
        return  Vector3.Distance(transform.position, _player.gameObject.transform.position);
    }

    public void SetStop(bool isStop)
    {
        if(isStop)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }  
        else
        {
            agent.isStopped = false;
        }
    }

    void Update()
    {
        if (!onPatternNormalCoolTime)
        {
            if (patternNormalCoolTime < patternNormal.coolTime)
            {
                patternNormalCoolTime += Time.deltaTime;
            }
            else
            {
                onPatternNormalCoolTime = true;
                patternNormalCoolTime = 0;
            }
        }
        if (!onPattern1and2CoolTime)
        {
            if (pattern1and2CoolTime < pattern1.coolTime)
            {
                pattern1and2CoolTime += Time.deltaTime;
            }
            else
            {
                onPattern1and2CoolTime = true;
                pattern1and2CoolTime = 0;
            }
        }
        
        if (!onPattern3CoolTime)
        {
            if (pattern3CoolTime < pattern3.coolTime)
            {
                pattern3CoolTime += Time.deltaTime;
            }
            else
            {
                onPattern3CoolTime = true;
                pattern3CoolTime = 0;
            }
        }
    }
    void FixedUpdate()
    {
        if (!_isRunning) return;
        _stateMachine.Update();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        stateMachine.TransitionTo(stateMachine.attackState);
    //    }
    //}
}