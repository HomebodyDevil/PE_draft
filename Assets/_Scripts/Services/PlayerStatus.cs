using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public float MaxCost { get; set; }
    
    public List<Card> PlayerDeck { get; private set; } = new();

    public PlayerStatus(
        PlayerStatusData data
    )
    {
        CurrentHealth = data.MaxHealth;
        MaxHealth = data.MaxHealth;
        MaxCost = data.MaxCost;
    }

    public void AddCardToPlayerDeck(Card newCard)
    {
        PlayerDeck.Add(newCard);
    }
}
