using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : Singleton<PlayerSystem>
{
    public List<Character> PlayerCharacters { get; private set; } = new();
}
