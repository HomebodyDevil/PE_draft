using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPortraitDialogueAction : DialogueAction
{
    private int _pos = 0;
    private string _characterName = "Character";
    private string _initialPortraitPath = "";
    
    public override Coroutine Execute(List<string> strArgs)
    {
        Validate(strArgs);
        StringToArgs(strArgs);
        
        return CoroutineRunnerService.Instance.StartCoroutine(ExecuteCoroutine());
    }
    
    protected override IEnumerator ExecuteCoroutine()
    {
        DialogueSystem.Instance.AddPortrait(
            _pos,
            _characterName,
            _initialPortraitPath
            );

        yield break;
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
        if (args.Count >= 1) _pos = StringToArg<int>(args[0]);
        if (args.Count >= 2) _characterName = StringToArg<string>(args[1]);
        if (args.Count >= 3) _initialPortraitPath = StringToArg<string>(args[2]);
    }
}
