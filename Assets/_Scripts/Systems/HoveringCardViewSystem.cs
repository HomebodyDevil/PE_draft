using System;
using UnityEngine;

public class HoveringCardViewSystem : Singleton<HoveringCardViewSystem>
{
    public Action<bool> OnSetHoveringCardViewVisible;
    public Action<Vector3> OnSetHoveringCardViewPos;
    public Action<Card> OnSetCurrentHoveringCard;
    
    [SerializeField] private CardView _hoveringCardView;
    public Card _currentHoveringCard { get; private set; }
    public bool _dragging { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        transform.AssignChildVar<CardView>("HoveringCardView", ref _hoveringCardView);
    }

    private void OnEnable()
    {
        OnSetHoveringCardViewVisible += SetHoveringCardViewVisible;
        OnSetHoveringCardViewPos += SetHoveringCardViewPos;
        OnSetCurrentHoveringCard += SetCurrentHoveringCard;
    }

    private void OnDisable()
    {
        OnSetHoveringCardViewVisible -= SetHoveringCardViewVisible;
        OnSetHoveringCardViewPos -= SetHoveringCardViewPos;
        OnSetCurrentHoveringCard -= SetCurrentHoveringCard;
    }

    private void Start()
    {
        SetHoveringCardViewVisible(false);
    }

    private void SetHoveringCardViewVisible(bool visible)
    {
        _hoveringCardView.SetVisible(visible);
    }

    private void SetHoveringCardViewPos(Vector3 worldPos)
    {
        _hoveringCardView.transform.position = worldPos;
    }

    private void SetCurrentHoveringCard(Card card)
    {
        _currentHoveringCard = card;
        _hoveringCardView.SetCardViewVisuals(card);
    }
}
