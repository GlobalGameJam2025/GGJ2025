using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform _target; 
    [SerializeField]
    private RectTransform _uiElement; 
    [SerializeField]
    private List<GameObject> _staminaBar; 
    private int _staminaindex = 2;
    private Vector3 _offset = new Vector3(0, 1, 0);
    private Vector2 _moveInput;
    [Header("ȸ�� ����")]
    [SerializeField]
    private float _dodgeSppedRate = 1;
    [SerializeField]
    private float _dodgeDuration = 0.2f;

    private void Start()
    {

        StartCoroutine(RecoverStamina());
    }


    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        //Debug.Log($"Move Input: {moveInput}");
    }

    public void OnDodge(InputValue value)
    {
        if (value.isPressed && _staminaindex > -1)
        {
            _staminaBar[_staminaindex].SetActive(false);
            _staminaindex--;
            //_sppedRate = 2f; // ��ư�� ������ �� �ӵ� ����
            StartCoroutine(Dodge());

            //StartCoroutine
        }
    }

    private IEnumerator Dodge()
    {
        Debug.Log("OnDodge!");
        float elapsedTime = 0f;

        // ȸ���ϴ� ���� ������ �̵�
        while (elapsedTime < _dodgeDuration)
        {
            _dodgeSppedRate = 3;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _dodgeSppedRate = 1;

        //if(_staminaindex < _staminaBar.Count - 1)
        //{
        //    yield return new WaitForSeconds(3f); // 3�� ���
        //    _staminaindex++;
        //    _staminaBar[_staminaindex].SetActive(true); // ���¹̳� ȸ��
        //    Debug.Log($"Stamina Recovered: {_staminaindex + 1}");
        //}
    }



    private IEnumerator RecoverStamina()
    {

        while (true)
        {
            yield return null;
            if(_staminaindex < _staminaBar.Count - 1)
            {
                yield return new WaitForSeconds(3f); // 3�� ���
                _staminaindex++;
                _staminaBar[_staminaindex].SetActive(true); // ���¹̳� ȸ��
                Debug.Log($"Stamina Recovered: {_staminaindex + 1}");
            }
           
        }

    }

    private void Update()
    {
        // ��: moveInput ���� ����� ĳ���� �̵� ó��
        transform.Translate(new Vector3(_moveInput.x, _moveInput.y) * _dodgeSppedRate * Time.deltaTime * 5f);


        // ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
        Vector3 worldPosition = _target.position + _offset ;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        _uiElement.position = screenPosition;
    }
}

