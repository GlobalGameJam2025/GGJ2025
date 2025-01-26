using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    [SerializeField] private float _hp = 100;
    [SerializeField]
    private RectTransform _uiHPElement;
    private Vector3 _offset = new Vector3(0, 1.5f, 0);

    public void OnDamage(float damage)
    {
        _hp -= damage;
        if (_hp >= 0f)
        {
            _uiHPElement.GetComponent<Image>().fillAmount = _hp/100;    
        }
        else
        {
            gameObject.SetActive(false);
            SceneLoader.instance.LoadScene("GameClear");
        }
    }

    private void Update()
    {
        //체력바 위치 
        Vector3 worldPosition = transform.position + _offset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        _uiHPElement.position = screenPosition;
    }
}