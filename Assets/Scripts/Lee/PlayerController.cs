using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _uiElement;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _mousePointer;
    [SerializeField]
    private List<GameObject> _staminaBar;
    public GameObject waterDefense;

    [Header("회피 설정")]
    [SerializeField]
    private float _dodgeSppedRate = 3;
    [SerializeField]
    private float _dodgeDuration = 0.2f;

    [Header("투사체 설정")]
    [SerializeField]
    private List<Projectile> _projectilesList;
    [SerializeField]
    private List<Projectile> _waterprojectilesList;
    private int _projectileIndex = 0;
    private int _waterprojectileIndex = 0;
    private float _dodgeSpped = 1;
    private int _staminaindex = 2;
    private Vector3 _offset = new Vector3(0, 1, 0);
    private Vector2 _moveInput;
    private Vector3 direction;

    public enum EGunState
    {
        Normal,       
        Water       
    }

    EGunState eGunState = EGunState.Normal;

    private void Start()
    {
        //_projectilesList = new List<Projectile>();
        _animator.SetBool("Move_S", true);
        StartCoroutine(RecoverStamina());
    }


    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        //Debug.Log($"Move Input: {_moveInput}");

        if (_moveInput.x ==1 && _moveInput.y ==0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            _animator.SetBool("Move_D", true);

            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_WS", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x == -1 && _moveInput.y == 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            _animator.SetBool("Move_D", true);

            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_WS", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x == 0 && _moveInput.y == 1)
        {
            _animator.SetBool("Move_W", true);

            _animator.SetBool("Move_WS", false);
            _animator.SetBool("Move_D", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x >0 && _moveInput.y > 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            _animator.SetBool("Move_WS", true);

            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_D", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x < 0 && _moveInput.y > 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            _animator.SetBool("Move_WS", true);

            _animator.SetBool("Move_W", false);
 
            _animator.SetBool("Move_D", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x > 0 && _moveInput.y < 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            _animator.SetBool("Move_DS", true);

            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_WS", false);
            _animator.SetBool("Move_D", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x < 0 && _moveInput.y < 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            _animator.SetBool("Move_DS", true);

            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_WS", false);
            _animator.SetBool("Move_D", false);
            
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x == 0 && _moveInput.y == 0)
        {
            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_WS", false);
            _animator.SetBool("Move_D", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", true);
        }

        //var currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0); // 0은 기본 레이어
        //int currentStateHash = currentStateInfo.shortNameHash;

        ////현재 상태의 이름 확인
        //Debug.Log($"Current State Hash: {currentStateHash}");

        //if (currentStateHash == Animator.StringToHash("temp"))
        //{
        //    Debug.Log("qweqew");
        //    _animator.SetBool("Move_W", false);
        //    _animator.SetBool("Move_D", false);
        //    _animator.SetBool("Move_DS", false);
        //}
    }

    public void OnDodge(InputValue value)
    {
        if (value.isPressed && _staminaindex > -1)
        {
            _staminaBar[_staminaindex].SetActive(false);
            _staminaindex--;
            StartCoroutine(Dodge());

        }
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed )
        {
            direction = (_mousePointer.position - transform.position).normalized;

            switch(eGunState)
            {
                case EGunState.Normal:
                    _projectilesList[_projectileIndex].Init(direction, transform.position);
                    if (_projectileIndex == _projectilesList.Count - 1)
                    {
                        _projectileIndex = 0;
                    }
                    else
                    {
                        _projectileIndex++;

                    }
                    break;

                case EGunState.Water:
                    _waterprojectilesList[_waterprojectileIndex].Init(direction, transform.position);
                    if (_waterprojectileIndex == _waterprojectilesList.Count - 1)
                    {
                        _waterprojectileIndex = 0;
                    }
                    else
                    {
                        _waterprojectileIndex++;

                    }
                    break;
            }
        }
    }
 

    public void OnChangeGun(InputValue value)
    {
        if (value.isPressed)
        {
            eGunState = (EGunState)(((int)eGunState + 1) % System.Enum.GetValues(typeof(EGunState)).Length);

            Debug.Log("현재 상태: " + eGunState);
        }
    }

    private IEnumerator Dodge()
    {
        Debug.Log("OnDodge!");
        float elapsedTime = 0f;

        // 회피하는 동안 빠르게 이동
        while (elapsedTime < _dodgeDuration)
        {
            _dodgeSpped = _dodgeSppedRate;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _dodgeSpped = 1;

    }

    private IEnumerator RecoverStamina()
    {

        while (true)
        {
            yield return null;
            if(_staminaindex < _staminaBar.Count - 1)
            {
                yield return new WaitForSeconds(3f); // 3초 대기
                _staminaindex++;
                _staminaBar[_staminaindex].SetActive(true); // 스태미나 회복
                Debug.Log($"Stamina Recovered: {_staminaindex + 1}");
            }
           
        }
    }

    public void SetHp(float damage)
    {
        if(waterDefense.activeSelf)
        {
            _uiElement.GetComponent<Image>().fillAmount -= damage * 0.2f;
            waterDefense.SetActive(false);
        }
        else
        {
            _uiElement.GetComponent<Image>().fillAmount -= damage;
        }
    }


    private void Update()
    {

        if (_uiElement.GetComponent<Image>().fillAmount <= 0)
            return;

        //마우스 포인터
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePointer.transform.position = new Vector3(worldPos.x, worldPos.y, -1);

        //플레이어 이동 
        transform.Translate(new Vector3(_moveInput.x, _moveInput.y) * _dodgeSpped * Time.deltaTime * 5f);

        //체력바 위치 
        Vector3 worldPosition = transform.position + _offset ;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        _uiElement.position = screenPosition;
       
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log($"{other.gameObject.name} has entered the 2D trigger!");
    //}

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log($"{other.gameObject.name} is staying in the 2D trigger!");
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    Debug.Log($"{other.gameObject.name} has exited the 2D trigger!");
    //}

}

