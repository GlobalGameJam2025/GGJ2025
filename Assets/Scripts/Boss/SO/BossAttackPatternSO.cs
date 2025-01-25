using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttackPatternSO : ScriptableObject
{
    [SerializeField] private string _description;

    [SerializeField] private float _attackDamage;
    public float attackArea;
    public float attackDamage => _attackDamage;
    public float coolTime;
    public abstract IEnumerator Attack(GameObject player, GameObject boss);
}
