using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBubble : Bubble
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private Transform _boss;

    protected override void TriggerBubble()
    {
        base.TriggerBubble();
        if (_boss != null && _fire != null && bombCount >= 5)
        {
            _fire.SetActive(true);
            Vector3 direction = (_boss.position - gameObject.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _fire.transform.position = gameObject.transform.position;
            _fire.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        }
    }
}
