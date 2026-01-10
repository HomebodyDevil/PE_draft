using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DialogueSystem에 변수로 선언하여 사용하고자 함.
public class DialogueActionExecutor
{
    // <Function-ActionName, Functions owner(Object)>
    private readonly Dictionary<string, DialogueAction> _actions = new()
    {
        { "AddPortrait", new AddPortraitDialogueAction() }
    };

    // 수행중인 Action을 hold하고 있도록 한 건데, 필요할지 모르겠음.
    //private readonly Dictionary<string, List<Coroutine>> _performing = new();

    public void ExecuteDialogueAction(string functionName, object[] args)
    {
        if (_actions.TryGetValue(functionName, out DialogueAction action))
        {
            action.Execute(args);
        }
        else
        {
            Debug.Log("No matched dialogue action performer");
        }
    }
}