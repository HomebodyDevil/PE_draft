using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestGA : GameAbility
{
    public string Text { get; private set; }

    public TestGA() { }
    
    public TestGA(string txt)
    {
        Text = txt;
    }
}

public class DebugHelperService : PersistantSingleton<DebugHelperService>
{
    private TextMeshProUGUI _text;

    protected override void Awake()
    {
        base.Awake();
        
        if (_text == null) transform.AssignChildVar<TextMeshProUGUI>("Text", ref _text);
    }

    private void Start()
    {
        GameAbilitySystem.Instance.AddPerformer<TestGA>(TestGAPerformer);
    }

    private IEnumerator TestGAPerformer(TestGA testGA)
    {
        _text.text = testGA.Text;
        yield return new WaitForSeconds(0.7f);
        
        _text.text = "Debug TEXT";
    }
}
