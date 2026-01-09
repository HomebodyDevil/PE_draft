using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : Singleton<DialogueSystem>
{
    [Header("Initialize Settings")] 
    [SerializeField] private bool _InitialVisible = false;
    [Space(10f)]
    
    [SerializeField] private Transform _dialogueCanvas;
    [SerializeField] private Transform _dialoguePanel;
    [SerializeField] private Transform _backgroundPanel;
    [SerializeField] private Transform _choiceButtonsPanel;
    [SerializeField] private Transform _speakerImages;
    [SerializeField] private Transform _speakerImageLeftPos;
    [SerializeField] private Transform _speakerImageRightPos;
    [SerializeField] private TextMeshProUGUI _speakerNameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private Button _clickCatcher;
    [SerializeField] private Transform _clickPreventer;

    private Coroutine _playDialogueCoroutine;
    private DialogueLine _currentDialogueLine;
    private string _currentDialogue;

    [SerializeField] private List<ChoiceButton> _choiceButtons = new(); 
    
    protected override void Awake()
    {
        base.Awake();
        SetVars();
    }

    private void Start()
    {
        _backgroundPanel.gameObject.SetActive(false);
        _choiceButtonsPanel.gameObject.SetActive(false);
        _clickCatcher.onClick.AddListener(OnClickClockCatcher);
        
        SetDialogueVisible(_InitialVisible);
    }

    private void OnEnable()
    {
        DialogueService.Instance.OnSetCurrentDialogueLine += SetCurrentDialogueLine;
        DialogueService.Instance.OnSetSpeakerName += SetSpeakerName;
        DialogueService.Instance.OnSetDialogueVisible += SetDialogueVisible;
        DialogueService.Instance.OnEnableClickPreventer += SetClickPreventer;
        DialogueService.Instance.OnPlayDialogue += PlayDialogue;
        DialogueService.Instance.OnMadeChoice += OnMadeChoice;
    }

    private void OnDisable()
    {
        DialogueService.Instance.OnSetCurrentDialogueLine -= SetCurrentDialogueLine;
        DialogueService.Instance.OnSetSpeakerName -= SetSpeakerName;
        DialogueService.Instance.OnSetDialogueVisible -= SetDialogueVisible;
        DialogueService.Instance.OnEnableClickPreventer -= SetClickPreventer;
        DialogueService.Instance.OnPlayDialogue -= PlayDialogue;
        DialogueService.Instance.OnMadeChoice -= OnMadeChoice;
    }

    private void SetVars()
    {
        if (_dialogueCanvas == null) transform.AssignChildVar<Transform>("DialogueCanvas", ref _dialogueCanvas);
        if (_dialoguePanel == null) transform.AssignChildVar<Transform>("DialoguePanel", ref _dialoguePanel);
        if (_backgroundPanel == null) transform.AssignChildVar<Transform>("BackgroundPanel", ref _backgroundPanel);
        if (_dialogueText == null) transform.AssignChildVar<TextMeshProUGUI>("DialogueText", ref _dialogueText);
        if (_speakerImages == null) transform.AssignChildVar<Transform>("SpeakerImages", ref _speakerImages);
        if (_speakerImageLeftPos == null) transform.AssignChildVar<Transform>("SpeakerImageLeftPos", ref _speakerImageLeftPos);
        if (_speakerImageRightPos == null) transform.AssignChildVar<Transform>("SpeakerImageRightPos", ref _speakerImageRightPos);
        if (_speakerNameText == null) transform.AssignChildVar<TextMeshProUGUI>("SpeakerNameText", ref _speakerNameText);
        if (_clickCatcher == null) transform.AssignChildVar<Button>("ClickCatcher", ref _clickCatcher);
        if (_choiceButtonsPanel == null) transform.AssignChildVar<Transform>("ChoiceButtonsPanel", ref _choiceButtonsPanel);
        if (_clickPreventer == null) transform.AssignChildVar<Transform>("ClickPreventer", ref _clickPreventer);

        foreach (ChoiceButton button in _choiceButtonsPanel.GetComponentsInChildren<ChoiceButton>(includeInactive: true))
        {
            _choiceButtons.Add(button);
        }
    }

    private void SetClickPreventer(bool enable)
    {
        _clickPreventer.gameObject.SetActive(enable);
    }

    private void SetDialogueVisible(bool visible)
    {
        _dialogueCanvas.gameObject.SetActive(visible);
    }
    
    private void OnClickClockCatcher()
    {
        DialogueService.Instance.OnDialogueClick?.Invoke();
    }

    private void SetCurrentDialogueLine(DialogueLine dialogueLine)
    {
        if (dialogueLine == null) return;
        
        _currentDialogueLine =  dialogueLine;
        
        SetSpeakerName(_currentDialogueLine.Speaker);
        SetDialogueText(_currentDialogueLine.DialogueText);
    }

    private void SetSpeakerName(string speakerName)
    {
        _speakerNameText.text = speakerName;
    }

    private void SetDialogueText(string dialogueText)
    {
        _dialogueText.text = dialogueText;
    }

    private void DisableChoiceButtons()
    {
        foreach (var choiceButton in _choiceButtons)
            choiceButton.gameObject.SetActive(false);
    }
    
    private void OnMadeChoice()
    {
        _choiceButtonsPanel.gameObject.SetActive(false);
    }

    private void SetChoices(DialogueLine dialogueLine)
    {
        if (dialogueLine.Choices.Count == 0) return;

        DisableChoiceButtons();
        
        List<DialogueLine> choicesLine = new();
        for (int i = 0; i < dialogueLine.Choices.Count; i++)
        {
            int choiceLineId = dialogueLine.Choices[i];
            foreach (var line in DialogueService.Instance.CurrentDialogueLines)
            {
                if (line.Id == choiceLineId) choicesLine.Add(line);
            }
        }
        
        for (int i = 0; i < choicesLine.Count; i++)
        {
            _choiceButtons[i].gameObject.SetActive(true);
            _choiceButtons[i].SetChoiceButton(choicesLine[i]);
        }
    }

    private void PlayDialogue()
    {
        if (_playDialogueCoroutine != null)
        {
            StopCoroutine(_playDialogueCoroutine);
            _playDialogueCoroutine = null;
        }
        _playDialogueCoroutine = StartCoroutine(PlayDialogueCoroutine());
    }

    private IEnumerator PlayDialogueCoroutine()
    {
        _playDialogueCoroutine = null;

        if (_currentDialogueLine.Choices.Count > 0)
        {
            _choiceButtonsPanel.gameObject.SetActive(true);
            SetChoices(_currentDialogueLine);
        }
        
        yield break;
    }
}
