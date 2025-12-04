using UnityEngine;

public enum CardType
{
    Playable,
    Targetable,
}

public class Card
{
    public CardType type { get; private set; }

    public Card(
        CardType type
    )
    {
        this.type = type;
    }
}
