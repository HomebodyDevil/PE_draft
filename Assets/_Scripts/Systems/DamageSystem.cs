using System.Collections;
using UnityEngine;

public class DamageSystem : Singleton<DamageSystem>
{
    private void OnEnable()
    {
        GameAbilitySystem.Instance?.AddPerformer<DealDamageGA>(DealDamagePerformer);
    }

    private void OnDisable()
    {
        GameAbilitySystem.Instance?.RemovePerformer<DealDamageGA>();
    }

    public IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        Debug.Log("Performing DealDamage");
            
        yield break;
    }
}
