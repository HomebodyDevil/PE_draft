using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class CardViewSystem : Singleton<CardViewSystem>
{
    public Action OnCardViewCreated;
    
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Camera cardViewCam;
    [SerializeField] private Transform cardViews;
    [SerializeField] private GameObject cardViewPrefab;
    [SerializeField] private RectTransform deckButtonRT;
    [SerializeField] private RectTransform graveyardButtonRT;
    [SerializeField] private Canvas cardViewCanvas;

    [SerializeField] private List<CardView> _usingCardViewList = new();
    [SerializeField] private List<CardView> _storedCardViewList = new();

    private Coroutine _lineupCoroutine;
    
    protected override void Awake()
    {
        base.Awake();
        VarSetup();
        Setup();
    }

    private void OnEnable()
    {
        PlayerCardSystem.Instance.OnDrawCard += OnDrawCard;
    }

    private void OnDisable()
    {
        PlayerCardSystem.Instance.OnDrawCard -= OnDrawCard;
    }

    // private void Start()
    // {
    //     
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
            _usingCardViewList.Add(cardViewComp);

            return cardViewComp;
        }
        else
        {
            Debug.Log("Fail ed to create card view");
            return null;
        }
    }

    private void OnDrawCard(InBattleCard cardView)
    {
        // 1. cardView를 얻어온다 : 남은 게 있다면 사용. 없다면 생성.
        //      -> CreateCardView
    }

    private CardView GetUseableCardView()
    {
        CardView ret = null;
        
        if (_storedCardViewList.Count > 0)
        {
            ret = _storedCardViewList[0];
            
            _usingCardViewList.Add(_storedCardViewList[0]);
            _storedCardViewList.RemoveAt(0);
        }
        else
        {
            ret = CreateCardView(deckButtonRT.localPosition);
        }

        return ret;
    }

    private void MoveCardToHand(RectTransform cardViewRT)
    {
        cardViewRT.DOLocalMove(graveyardButtonRT.localPosition, 0.5f);
    }

    private IEnumerator LineUpHandCardViews()
    {
        int cardNum = _usingCardViewList.Count;

        float space = cardNum > ConstValue.MAX_HAND_CARDS
            ? (1f / (cardNum - 1))
            : (1f / (ConstValue.MAX_HAND_CARDS - 1));

        float firstCardPos = 0.5f - (cardNum - 1) * space * 0.5f;
        
        for (int i = 0; i < cardNum; i++)
        {
            RectTransform cardViewRT = _usingCardViewList[i].GetComponent<RectTransform>();
            if (cardViewRT == null)
            {
                Debug.Log($"CardView[{i}] is null");
                continue;
            }
            
            float pos = firstCardPos + space * i;
            float rot = -(2 * ConstValue.CARDVIEW_ROTATE_MAX * pos - ConstValue.CARDVIEW_ROTATE_MAX);
                
            Vector3 worldPos = splineContainer.EvaluatePosition(pos);
            
            cardViewRT.DOScale(Vector3.one, ConstValue.CARD_LINEUP_SECONDS);
            cardViewRT.DORotate(new(0, 0, rot), ConstValue.CARD_LINEUP_SECONDS);
            cardViewRT.DOMove(worldPos, ConstValue.CARD_LINEUP_SECONDS);
        }

        yield break;
    }

    public void CreateCardViewAddToHand()
    {
        CreateCardView(deckButtonRT.localPosition);
        if (_lineupCoroutine != null) StopCoroutine(_lineupCoroutine);
        _lineupCoroutine = StartCoroutine(LineUpHandCardViews());
    }
}