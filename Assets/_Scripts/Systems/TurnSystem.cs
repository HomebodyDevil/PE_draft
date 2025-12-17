using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    private List<Character> _charactersTurnOrder = new();

    private void Start()
    {
        SetInitialTurnOrder();
    }

    public void SetInitialTurnOrder()
    {
        _charactersTurnOrder.AddRange(PlayerSystem.Instance.PlayerCharacters);
        _charactersTurnOrder.AddRange(EnemySystem.Instance.EnemyCharacters);
    }
}
