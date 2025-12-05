using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="PlayerStatusData", menuName="Data/PlayerStatusData")]
public class PlayerStatusData : ScriptableObject
{
    [field: SerializeField] public float MaxHealth;
    [field: SerializeField] public float MaxCost;
    [field: SerializeField] public List<CardData> Deck;
}
