using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class CardViewSystem : Singleton<CardViewSystem>
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Camera cardViewCam;
    [SerializeField] private Transform cardViews;
    [SerializeField] private GameObject cardViewPrefab;
    [SerializeField] private RectTransform deckButtonRT;
    [SerializeField] private RectTransform graveyardButtonRT;
    [SerializeField] private Canvas cardViewCanvas;

    [SerializeField] private List<CardView> _cardViewList = new();

    protected override void Awake()
    {
        base.Awake();
        VarSetup();
        Setup();
    }

    // private void Start()
    // {
    //     CreateCardView(deckButtonRT.localPosition);
    //     MoveCardToHand(_cardViewList[0].GetComponent<RectTransform>());
    // }

    private void VarSetup()
    {
        if (splineContainer == null) transform.AssignChildVar<SplineContainer>("CardViewCurve", ref splineContainer);
        if (cardViewCam == null) transform.AssignChildVar<Camera>("CardViewCamera", ref cardViewCam);
        if (cardViews == null) transform.AssignChildVar<Transform>("CardViews", ref cardViews);
        if (deckButtonRT == null) transform.AssignChildVar<RectTransform>("DeckButtonRT", ref deckButtonRT);
        if (graveyardButtonRT == null)
            transform.AssignChildVar<RectTransform>("GraveyardButtonRT", ref graveyardButtonRT);
        if (cardViewCanvas == null) transform.AssignChildVar<Canvas>("CardViewCanvas", ref cardViewCanvas);
    }

    private void Setup()
    {
        if (cardViewCanvas != null)
        {
            cardViewCanvas.sortingOrder = ConstValue.CARDVIEW_ORDER;
        }
    }

    /// <summary></summary>
    /// <param name="localPosition">PosX=0, PosY=0에 대한 localPosition.</param>
    private CardView CreateCardView(Vector3 localPosition)
    {
        GameObject cardView = Instantiate(cardViewPrefab, cardViews, false);
        cardView?.GetComponent<RectTransform>().SetLocalPositionAndRotation(localPosition, Quaternion.identity);

        if (cardView.TryGetComponent(out CardView cardViewComp))
        {
            cardView.GetComponent<RectTransform>().localScale = Vector3.zero;
            _cardViewList.Add(cardViewComp);

            return cardViewComp;
        }
        else
        {
            Debug.Log("Fail ed to create card view");
            return null;
        }
    }

    private void MoveCardToHand(RectTransform cardViewRT)
    {
        cardViewRT.DOLocalMove(graveyardButtonRT.localPosition, 0.5f);
    }

    private IEnumerator LineUpHandCardViews()
    {
        int cardNum = _cardViewList.Count;

        float space = cardNum > ConstValue.MAX_HAND_CARDS
            ? (1f / (cardNum - 1))
            : (1f / (ConstValue.MAX_HAND_CARDS - 1));

        float firstCardPos = 0.5f - (cardNum - 1) * space * 0.5f;
        
        for (int i = 0; i < cardNum; i++)
        {
            RectTransform cardViewRT = _cardViewList[i].GetComponent<RectTransform>();
            if (cardViewRT == null)
            {
                Debug.Log($"CardView[{i}] is null");
                continue;
            }
        }

        yield break;
    }
}