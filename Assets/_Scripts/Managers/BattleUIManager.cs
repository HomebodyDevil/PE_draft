using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : Singleton<BattleUIManager>
{
    [SerializeField] private Canvas _buttonsUiCanvas;
    [SerializeField] private Button _deckButton;
    [SerializeField] private Button _graveyardButton;

    private List<Character> _playerCharacters = new();
    private List<Character> _enemyCharacters = new();
    
    protected override void Awake()
    {
        base.Awake();

        VarSetup();
        Setup();
    }

    private void OnEnable()
    {
        _deckButton.onClick.AddListener(
            () => PersistantUIManager.Instance.OnShowCardsExhibition.Invoke(true, ShowCards.Deck));
        _graveyardButton.onClick.AddListener(
            () => PersistantUIManager.Instance.OnShowCardsExhibition.Invoke(true, ShowCards.BattleGraveyard));
    }

    private void Foo()
    {
        
    }

    private void OnDisable()
    {
        _deckButton.onClick.RemoveAllListeners();
        _graveyardButton.onClick.RemoveAllListeners();
    }

    private void VarSetup()
    {
        if (_buttonsUiCanvas == null) transform.AssignChildVar<Canvas>("ButtonsUICanvas", ref _buttonsUiCanvas);
        if (_deckButton == null) transform.AssignChildVar<Button>("DeckButton", ref _deckButton);
        if (_graveyardButton == null) transform.AssignChildVar<Button>("GraveyardButton", ref _graveyardButton);
    }

    private void Setup()
    {
        if (_buttonsUiCanvas != null)
        {
            _buttonsUiCanvas.sortingOrder = ConstValue.UI_ORDER;
        }
    }
}
