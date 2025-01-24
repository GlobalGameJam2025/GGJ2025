using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform _target; 
    [SerializeField]
    private RectTransform _uiElement;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _mousePointer;
    [SerializeField]
    private GameObject _normalBubble;
    [SerializeField]
    private List<GameObject> _staminaBar;
    private int _staminaindex = 2;
    private Vector3 _offset = new Vector3(0, 1, 0);
    private Vector2 _moveInput;
    [Header("회피 설정")]
    [SerializeField]
    private float _dodgeSppedRate = 3;
    [SerializeField]
    private float _dodgeDuration = 0.2f;
    float _dodgeSpped = 1;


    public float decelerationRate = 0.1f; // 감속 비율
    public float minSpeed = 0.1f;      // 최소 속도

    private float currentSpeed = 10;        // 현재 속도
    private Vector3 direction;         // 초기 발사 방향

    private void Start()
    {
        _animator.SetBool("Move_S", true);
        // 초기 속도 설정

        StartCoroutine(RecoverStamina());

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

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        //Debug.Log($"Move Input: {_moveInput}");
   

        if (_moveInput.x ==1 && _moveInput.y ==0)
        {
            _target.GetComponent<SpriteRenderer>().flipX = false;
            _animator.SetBool("Move_D", true);

            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_WS", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x == -1 && _moveInput.y == 0)
        {
            _target.GetComponent<SpriteRenderer>().flipX = true;
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
            _target.GetComponent<SpriteRenderer>().flipX = false;
            _animator.SetBool("Move_WS", true);

            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_D", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x < 0 && _moveInput.y > 0)
        {
            _target.GetComponent<SpriteRenderer>().flipX = true;
            _animator.SetBool("Move_WS", true);

            _animator.SetBool("Move_W", false);
 
            _animator.SetBool("Move_D", false);
            _animator.SetBool("Move_DS", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x > 0 && _moveInput.y < 0)
        {
            _target.GetComponent<SpriteRenderer>().flipX = false;
            _animator.SetBool("Move_DS", true);

            _animator.SetBool("Move_W", false);
            _animator.SetBool("Move_WS", false);
            _animator.SetBool("Move_D", false);
            _animator.SetBool("Move_S", false);
        }
        else if (_moveInput.x < 0 && _moveInput.y < 0)
        {
            _target.GetComponent<SpriteRenderer>().flipX = true;
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

    private GameObject tempGameObject;

    public void OnFire(InputValue value)
    {
        if (value.isPressed )
        {
            direction = (_mousePointer.position - transform.position).normalized;
            tempGameObject = Instantiate(_normalBubble,_target.position,Quaternion.identity);
            tempGameObject.GetComponent<Projectile>().direction = direction;
            tempGameObject.GetComponent<Projectile>().isfire = true;
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
        _uiElement.GetComponent<Image>().fillAmount -= damage;
    }

    float elapsedTime = 0;

    private void Update()
    {

        if (_uiElement.GetComponent<Image>().fillAmount <= 0)
            return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePointer.transform.position = new Vector3(worldPos.x, worldPos.y, -1);
        // 예: moveInput 값을 사용해 캐릭터 이동 처리
        transform.Translate(new Vector3(_moveInput.x, _moveInput.y) * _dodgeSpped * Time.deltaTime * 5f);


        // 월드 좌표를 화면 좌표로 변환
        Vector3 worldPosition = _target.position + _offset ;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        _uiElement.position = screenPosition;


        //if(tempGameObject != null)
        //{
        //    // 지수적으로 감속
        //    currentSpeed = 20 * Mathf.Exp(-1 * Time.time);

        //    // 속도가 0 이하로 떨어지면 멈춤
        //    if (currentSpeed <= 0f)
        //    {
        //        currentSpeed = 0f;
        //        Debug.Log("Projectile stopped.");
        //        return;
        //    }

        //    // 방향으로 이동
        //    tempGameObject.transform.position += direction * currentSpeed * Time.deltaTime;
        //}

       
    }

}

