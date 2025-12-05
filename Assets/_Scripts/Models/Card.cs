using UnityEngine;

public enum ECardPlayType
{
    Playable,
    Targetable,
}

public class Card
{
    public ECardPlayType CardPlayType { get; private set; }

    public Card(
        ECardPlayType type
    )
    {
        this.CardPlayType = type;
    }
}
