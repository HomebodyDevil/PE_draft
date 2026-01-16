using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/BattleEnemies", fileName="BattleEnemiesData")]
public class BattleEnemiesData : ScriptableObject
{
    [SerializeField] public List<CharacterData> Enemies = new();
}
