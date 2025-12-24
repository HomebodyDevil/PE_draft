using UnityEngine;

public class DealDamageToRandomTargetsGA : GameAbility
{
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField] public int TargetCount { get; private set; }

    public DealDamageToRandomTargetsGA() { }
    
    public DealDamageToRandomTargetsGA(
        float baseDamage,
        int targetCount)
    {
        BaseDamage = baseDamage;
        TargetCount = targetCount;
    }
}
