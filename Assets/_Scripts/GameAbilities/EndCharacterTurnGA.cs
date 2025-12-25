using UnityEngine;

public class EndCharacterTurnGA : GameAbility
{
    public Character TurnCharacter;

    public EndCharacterTurnGA() { }
    
    public EndCharacterTurnGA(Character character)
    {
        TurnCharacter = character;
    }
}
