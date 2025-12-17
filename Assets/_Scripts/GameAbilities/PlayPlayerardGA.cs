using UnityEngine;

public class PlayPlayerardGA : GameAbility
{
    public InBattleCard InBattleCardToPlay { get; set; }

    public PlayPlayerardGA(InBattleCard inBattleCardToPlay)
    {
        InBattleCardToPlay = inBattleCardToPlay;
    }
}
