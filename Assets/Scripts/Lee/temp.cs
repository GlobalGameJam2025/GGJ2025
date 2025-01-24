using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(234);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(123213);
        Debug.Log($"{other.gameObject.name} has entered the 2D trigger!");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log($"{other.gameObject.name} is staying in the 2D trigger!");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"{other.gameObject.name} has exited the 2D trigger!");
    }
}
