using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPortraitDialogueAction : DialogueAction
{
    public override Coroutine Execute(List<string> strArgs)
    {
        Validate(strArgs);
        StringToArgs(strArgs);
        
        return CoroutineRunnerService.Instance.StartCoroutine(ExecuteCoroutine());
    }
    
    protected override IEnumerator ExecuteCoroutine()
    {
        throw new System.NotImplementedException();
    }

    protected override void CompleteAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void Validate(List<string> args)
    {
        Debug.Log("Validate arguments");
    }

    protected override void StringToArgs(List<string> args)
    {
        throw new System.NotImplementedException();
    }
}
