using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubble : Bubble
{
    [SerializeField]
    private GameObject _defenseIcon;
    [SerializeField]
    private PlayerController _playerController;

    protected override void TriggerBubble()
    {
        base.TriggerBubble();
        if (bombCount >= 5)
            _defenseIcon.SetActive(true);
    }

    protected override void TriggerPlayer()
    {
        base.TriggerPlayer();
        if (bombCount >= 5)
        {
            _playerController.waterDefense.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
