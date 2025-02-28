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
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] _audioClip;
    [SerializeField]
    private Image _bubbleImage;
    [SerializeField]
    private Image _gunImage;
    [SerializeField]
    private Sprite[] _bubbleSprite;
    [SerializeField]
    private Sprite[] _gunSprite;
    [SerializeField]
    private GameObject _playerDim;
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

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
    private List<Projectile> _waterProjectilesList;
    [SerializeField]
    private List<Projectile> _electricProjectilesList;
    [SerializeField]
    private List<Projectile> _fireProjectilesList;
    [SerializeField]
    private List<Gun> _gunProjectilesList;

    private int _projectileIndex = 0;
    private int _waterProjectileIndex = 0;
    private int _electricProjectileIndex = 0;
    private int _fireProjectileIndex = 0;
    private int _gunProjectileIndex = 0;
    private float _dodgeSpped = 1;
    private int _staminaindex = 2;
    private bool _isGun;
    private Vector3 _offset = new Vector3(0, 1, 0);
    private Vector2 _moveInput;
    private Vector3 direction;

    public enum EGunState
    {
        Normal,
        Electric,
        Fire,
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
        if (_uiElement.GetComponent<Image>().fillAmount <= 0)
        {
            return;
        }

        if (value.isPressed )
        {
            direction = (_mousePointer.position - transform.position).normalized;

            if(_isGun)
            {
                _gunProjectilesList[_gunProjectileIndex].Init(transform.position);
                if (_gunProjectileIndex == _gunProjectilesList.Count - 1)
                {
                    _gunProjectileIndex = 0;
                }
                else
                {
                    _gunProjectileIndex++;

                }
            }
            else
            {
                switch (eGunState)
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
                        _waterProjectilesList[_waterProjectileIndex].Init(direction, transform.position);
                        if (_waterProjectileIndex == _waterProjectilesList.Count - 1)
                        {
                            _waterProjectileIndex = 0;
                        }
                        else
                        {
                            _waterProjectileIndex++;

                        }
                        break;
                    case EGunState.Electric:
                        _electricProjectilesList[_electricProjectileIndex].Init(direction, transform.position);
                        if (_electricProjectileIndex == _electricProjectilesList.Count - 1)
                        {
                            _electricProjectileIndex = 0;
                        }
                        else
                        {
                            _electricProjectileIndex++;

                        }
                        break;
                    case EGunState.Fire:
                        _fireProjectilesList[_fireProjectileIndex].Init(direction, transform.position);
                        if (_fireProjectileIndex == _fireProjectilesList.Count - 1)
                        {
                            _fireProjectileIndex = 0;
                        }
                        else
                        {
                            _fireProjectileIndex++;


                        }
                        break;
                }
            }
        }
    }

    public void OnKey1(InputValue value)
    {
        if (value.isPressed)
        {
            _isGun = false;
            _gunImage.sprite = _gunSprite[0];
        }
    }

    public void OnKey2(InputValue value)
    {
        if (value.isPressed)
        {
            _isGun = true;
            _gunImage.sprite = _gunSprite[1];
        }
    }


    public void OnChangeGun(InputValue value)
    {
        if (value.isPressed && !_isGun)
        {
            eGunState = (EGunState)(((int)eGunState + 1) % System.Enum.GetValues(typeof(EGunState)).Length);
            _bubbleImage.sprite = _bubbleSprite[(int)eGunState];

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

    public IEnumerator OnPleyerDim()
    {
        _playerDim.SetActive(true);
        yield return new WaitForSeconds(2);
        _playerDim.SetActive(false);

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

    public void ApplyKnockback(Vector3 sourcePosition)
    {
            // 넉백 방향 계산: 대상에서 현재 오브젝트를 향하는 방향
            Vector3 direction = (transform.position - sourcePosition).normalized;

            // 넉백 적용
            _rigidbody2D.AddForce(direction * 5, ForceMode2D.Impulse);
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


        if (_uiElement.GetComponent<Image>().fillAmount <= 0)
        {
            //플레이어 죽음
            SceneLoader.instance.LoadScene("GameOver");
            _audioSource.clip = _audioClip[1];
            _audioSource.Play();
        }
        else
        {
            //플레이어 히트 
            _audioSource.clip = _audioClip[0];
            _audioSource.Play();
        }

    
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.V))
            ApplyKnockback(new Vector3(20, 20, 1));

        if (_uiElement.GetComponent<Image>().fillAmount <= 0)
        {
            _audioSource.clip = _audioClip[1];
            _audioSource.Play();
            return;
        }

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