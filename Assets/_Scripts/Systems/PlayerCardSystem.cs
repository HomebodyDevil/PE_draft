using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardSystem : Singleton<PlayerCardSystem>
{
    public Action<InBattleCard> OnDrawCard;
    public Action<InBattleCard> OnMoveCardToGraveyard;
    
    public List<InBattleCard> Deck { get; private set; } = new();
    public List<InBattleCard> Hand { get; private set; } = new();
    public List<InBattleCard> Graveyard { get; private set; } = new();

    private void Start()
    {
        SetupDeck();
    }

    private void SetupDeck()
    {
        foreach (var card in PlayerStatusService.Instance.PlayerStatus.PlayerDeck)
        {
            Card cardCopy = new(card);    // 같은 값의 다른 객체.
            InBattleCard battleCard = new(cardCopy, card);
            
            Deck.Add(battleCard);
        }
    }

    public IEnumerator DrawCards(int count)
    {
        if (Deck.Count == 0 && Graveyard.Count == 0 && count > 0)
        {
            Debug.Log("Cant Draw Cards");
            yield break;
        }
        
        int possibleCount = Mathf.Min(count, Deck.Count);
        int remainingCount = count - possibleCount;

        int loopCnt = 0;
        do
        {
            if (loopCnt++ > ConstValue.MAX_LOOP)
            {
                Debug.Log("Too much Loop");
                break;
            }
            
            for (int i = 0; i < possibleCount; i++)
            {
                yield return DrawCard();
            }

            if (remainingCount > 0) RefillDeck();
            
            possibleCount = Mathf.Min(remainingCount, Deck.Count);
            remainingCount = count - possibleCount;
        } while (possibleCount > 0);
    }

    private IEnumerator DrawCard()
    {
        Hand.Add(Deck[0]);
        OnDrawCard?.Invoke(Deck[0]);
        
        OnDrawCard?.Invoke(Deck[0]);
        Deck.RemoveAt(0);

        yield break;
    }

    public IEnumerator MoveCardToGraveyard(InBattleCard card)
    {
        OnMoveCardToGraveyard?.Invoke(card);

        yield break;
    }

    public void RefillDeck()
    {
        Deck.AddRange(Graveyard);
        Deck.Shuffle();
        
        Graveyard.Clear();
    }
}
