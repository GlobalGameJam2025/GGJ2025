using UnityEngine;
using UnityEngine.InputSystem;

public class temp : MonoBehaviour
{
    private Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log($"Move Input: {moveInput}");
    }

    public void OnDodge(InputValue value)
    {
        Debug.Log(value);
        if (value.isPressed)
        {
            Debug.Log("Jump!");
        }
    }

    private void Update()
    {
        // ��: moveInput ���� ����� ĳ���� �̵� ó��
        transform.Translate(new Vector3(moveInput.x, moveInput.y) * Time.deltaTime * 5f);
    }
}
