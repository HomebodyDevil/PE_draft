using UnityEngine;

public class DiscardPlayerCardsGA : GameAbility
{
    public bool DiscardAll { get; set; }
    public int DiscardCount { get; set; }

    public DiscardPlayerCardsGA(bool discardAll, int discardCnt)
    {
        DiscardAll = discardAll;
        DiscardCount = discardCnt;
    }
}