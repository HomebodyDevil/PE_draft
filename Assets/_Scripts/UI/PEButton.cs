using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PEButton : Button
{
    protected TextMeshProUGUI _buttonText;

    protected override void OnDisable()
    {
        base.OnDisable();
        if (TryGetComponent<Button>(out var button))
        {
            button.onClick.RemoveAllListeners();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (_buttonText == null)
        {
            transform.AssignChildVar<TextMeshProUGUI>("ButtonText", ref _buttonText);
        }
    }

    public void SetButtonText(string text)
    {
        _buttonText.text = text;
    }

    public void SetOnButtonClick(Action action)
    {
        if (TryGetComponent<Button>(out var button))
        {
            // button은 UnityAction을 사용하기 때문에 감싸줄 필요가 있음.
            button.onClick.AddListener(() =>  action?.Invoke());
        }
    }
}
