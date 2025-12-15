using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DealDamageGA : TargetGameAbility
{
    [field: SerializeField] public float BaseDamage { get; private set; }
    
    public override void ExecuteGameAbility(Character executor)
    {
        Debug.Log($"{executor.gameObject.name} execute dealing damage");
    }
}
