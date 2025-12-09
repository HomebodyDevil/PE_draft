using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : Singleton<BattleSystem>
{
    public Action<Character> OnFoundTarget;
    public Action<Character> OnLostTarget;
    public Action OnClearTargets;
    
    private List<Character> _currentTargets = new();

    private void OnEnable()
    {
        OnFoundTarget += AddTarget;
        OnLostTarget += RemoveTarget;
        OnClearTargets += ClearTargets;
    }

    private void OnDisable()
    {
        OnFoundTarget -= AddTarget;
        OnLostTarget -= RemoveTarget;
        OnClearTargets -= ClearTargets;
    }

    private void AddTarget(Character target)
    {
        if (target != null) _currentTargets.Add(target);
    }

    private void RemoveTarget(Character target)
    {
        if (target != null) _currentTargets.Remove(target);
    }

    private void ClearTargets()
    {
        _currentTargets?.Clear();
    }
}
