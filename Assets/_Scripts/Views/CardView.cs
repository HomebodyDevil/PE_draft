using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Card _card { get; private set; }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_card.CardPlayType)
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
        switch (_card.CardPlayType)
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
        switch (_card.CardPlayType)
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
        Debug.Log("OnTargetablePointerDown");
    }
    
    private void OnPlayablePointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPlayablePointerUp");
    }

    private void OnTargetablePointerUp(PointerEventData eventData)
    {
        Debug.Log("OnTargetablePointerUp");
    }
    
    private void OnPlayablePointerDrag(PointerEventData eventData)
    {
        Debug.Log("OnPlayablePointerDrag");
    }

    private void OnTargetablePointerDrag(PointerEventData eventData)
    {
        Debug.Log("OnTargetablePointerDrag");
    }
}
