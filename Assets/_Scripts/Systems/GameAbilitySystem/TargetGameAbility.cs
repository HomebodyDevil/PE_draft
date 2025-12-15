using System.Collections.Generic;
using UnityEngine;

public class TargetGameAbility : GameAbility
{
    public List<Character> Targets { get; private set; }= new();

    public virtual void SetTargets(List<Character> targets)
    {
        Targets = targets;
    }
    
    public override void ExecuteGameAbility(Character executor)
    {
        
    }
}
