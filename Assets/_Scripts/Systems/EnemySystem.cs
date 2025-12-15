using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    public List<Character> EnemyCharacters { get; private set; } = new();
}
