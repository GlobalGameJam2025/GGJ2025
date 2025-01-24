using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _projectileSpeed = 20;
    private float _currentSpeed;
    private Vector3 _direction;
    private bool _isfire;
    private bool _isStop;
    private float _elapsedTime = 0f;
    private float _stopTime = 0f;

    public void Init(Vector3 direction ,Vector3 InitPos)
    {
        _isfire = true;
        _isStop = false;
        _stopTime = 0f;
        _elapsedTime = 0f;
        this._direction = direction; 
        gameObject.SetActive(true);
        transform.position = InitPos;   
    }
   

    private void Update()
    {
        if (_isfire)
        {
            _elapsedTime += Time.deltaTime;
            _currentSpeed = _projectileSpeed * Mathf.Exp(-3 * _elapsedTime);

            if (_currentSpeed <= 0.1f)
            {
                _currentSpeed = 0f;
                _isfire = false;
                _isStop = true;
            }
            transform.position += _direction * _currentSpeed * Time.deltaTime;
        }

        if(_isStop)
        {
            _stopTime += Time.deltaTime;

            if(_stopTime >1)
            {
                _isStop = false;
                _elapsedTime = 0f;
                gameObject.SetActive(false);
            }

        }
    }

}
