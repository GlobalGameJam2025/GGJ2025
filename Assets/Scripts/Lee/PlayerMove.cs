using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Vector2 _moveInput;
    private float _sppedRate = 1;
    private float _dodgeDuration = 0.2f;


    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        //Debug.Log($"Move Input: {moveInput}");
    }

    public void OnDodge(InputValue value)
    {
        if (value.isPressed)
        {
            //_sppedRate = 2f; // 버튼을 눌렀을 때 속도 증가
            StartCoroutine(Dodge());
        }
    }

    private IEnumerator Dodge()
    {
        Debug.Log("OnDodge!");
        float elapsedTime = 0f;

        // 회피하는 동안 빠르게 이동
        while (elapsedTime < _dodgeDuration)
        {
            _sppedRate = 3;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _sppedRate = 1;
    }

    private void Update()
    {

        //Debug.Log(_sppedRate);
        // 예: moveInput 값을 사용해 캐릭터 이동 처리
        transform.Translate(new Vector3(_moveInput.x, _moveInput.y) * _sppedRate * Time.deltaTime * 5f);
    }
}
