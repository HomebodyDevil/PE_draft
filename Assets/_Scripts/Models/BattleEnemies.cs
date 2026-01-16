using System.Collections.Generic;
using UnityEngine;

public class BattleEnemies
{
    public List<CharacterData> Enemies = new();
    
    public BattleEnemies(BattleEnemiesData data)
    {
        Enemies = new(data.Enemies);
    }
}
