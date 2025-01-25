using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBubble : Bubble
{
    [SerializeField] private GameObject _electric;
    [SerializeField] private Transform _boss;

    protected override void TriggerBubble()
    {
        base.TriggerBubble();
        if (_boss != null && _electric != null && bombCount >= 5)
        {
            _electric.SetActive(true);
            Vector3 direction = (_boss.position - gameObject.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _electric.transform.position = gameObject.transform.position;
            _electric.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            
        }
    }
}