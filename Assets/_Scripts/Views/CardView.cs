using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InBattleCard _card { get; private set; }

    [SerializeField] public TextMeshProUGUI cardNameTMP;
    [SerializeField] public TextMeshProUGUI cardDescriptionTMP;
    //[SerializeField] private TextMeshProUGUI costTMP;
    
    private void Awake()
    {
        transform.AssignChildVar<TextMeshProUGUI>("CardNameText", ref cardNameTMP);
        transform.AssignChildVar<TextMeshProUGUI>("CardDescriptionText", ref cardDescriptionTMP);
    }
    
    public void SetCardView(InBattleCard card)
    {
        _card = card;
        
        SetCardViewNameText(card.BattleCard.Name);
        SetCardViewDescriptionText(card.BattleCard.Description);
        SetCardViewCostText(card.BattleCard.Cost);
    }

    public void SetCardViewNameText(String newName)
    {
        if (cardNameTMP == null) return;
        
        cardNameTMP.text = newName;
    }

    public void SetCardViewDescriptionText(string newDescription)
    {
        if (cardDescriptionTMP == null) return;
        
        cardDescriptionTMP.text = newDescription;
    }

    public void SetCardViewCostText(int newCost)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_card.BattleCard.CardPlayType)
        {
            case ECardPlayType.Playable:
                OnPlayablePointerDown(eventData);
                break;
            case ECardPlayType.Targetable:
                OnTargetablePointerDown(eventData);
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (_card.BattleCard.CardPlayType)
        {
            case ECardPlayType.Playable:
                OnPlayablePointerUp(eventData);
                break;
            case ECardPlayType.Targetable:
                OnTargetablePointerUp(eventData);
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (_card.BattleCard.CardPlayType)
        {
            case ECardPlayType.Playable:
                OnPlayablePointerDrag(eventData);
                break;
            case ECardPlayType.Targetable:
                OnTargetablePointerDrag(eventData);
                break;
        }
    }
    
    private void OnPlayablePointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPlayablePointerDown");
    }

    private void OnTargetablePointerDown(PointerEventData eventData)
    {
        LineSystem.Instance?.SetVisible(true);
        LineSystem.Instance?.OnSetStartDrawLine?.Invoke(LineSystem.Instance.GetLinePointPosOfWorldPos(transform.position));
        LineSystem.Instance?.OnSetEndDrawLine?.Invoke(LineSystem.Instance.GetLinePointPosOfWorldPos(transform.position));
    }
    
    private void OnPlayablePointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPlayablePointerUp");
    }

    private void OnTargetablePointerUp(PointerEventData eventData)
    {
        // BattleSystem의 Target이 있다면, 해당 Target들에 대한 카드 Effect를 수행한다.
        // 이후, BattleSystem의 Target을 초기화 한다.
        
        LineSystem.Instance?.SetVisible(false);
    }
    
    private void OnPlayablePointerDrag(PointerEventData eventData)
    {
        Debug.Log("OnPlayablePointerDrag");
    }

    private void OnTargetablePointerDrag(PointerEventData eventData)
    {
        LineSystem.Instance?.OnSetEndDrawLine?.Invoke(LineSystem.Instance.GetLinePointPosOfMousePos());
        
        // 1. Raycast를 수행해서 BattleSystem의 Target을 설정할 수 있는지 확인한다.
    }
}
