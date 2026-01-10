using System.Collections;
using UnityEngine;

public class AddPortraitDialogueAction : DialogueAction
{
    public override Coroutine Execute(object[] args)
    {
        return CoroutineRunnerService.Instance.StartCoroutine(ExecuteCoroutine(args));
    }
    
    protected override IEnumerator ExecuteCoroutine(object[] args)
    {
        Validate(args);
        
        throw new System.NotImplementedException();
    }

    protected override void CompleteAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void Validate(object[] args)
    {
        Debug.Log("Validate arguments");
    }
}
