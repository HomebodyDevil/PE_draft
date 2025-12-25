using UnityEngine;

public class DrawCardsGA : GameAbility
{
    [field: SerializeField] public int DrawCount { get; private set; }

    public DrawCardsGA() { }
    
    public DrawCardsGA(int drawCount)
    {
        DrawCount = drawCount;
    }
}
