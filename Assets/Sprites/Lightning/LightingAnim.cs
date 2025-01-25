using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingAnim : MonoBehaviour
{
    public GameObject _parent;
    public void DeActivateParent()
    {
        _parent.SetActive(false);
    }
}
