using UnityEngine;

public class StartCharacterTurnGA : GameAbility
{
    public Character TurnCharacter;

    public StartCharacterTurnGA() { }
    
    public StartCharacterTurnGA(Character character)
    {
        TurnCharacter = character;
    }
}
