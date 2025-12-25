using UnityEngine;

public class DiscardPlayerCardsGA : GameAbility
{
    public bool DiscardAll { get; set; }
    public int DiscardCount { get; set; }

    public DiscardPlayerCardsGA(bool discardAll=false, int discardCnt=0)
    {
        DiscardAll = discardAll;
        DiscardCount = discardCnt;
    }
}