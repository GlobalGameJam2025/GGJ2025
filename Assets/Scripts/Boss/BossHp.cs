using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    [SerializeField] private float _hp = 100;

    public void OnDamage(float damage)
    {
        if (_hp > 0)
        {
            _hp -= damage;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}