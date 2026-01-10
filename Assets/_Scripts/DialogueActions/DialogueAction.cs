using System.Collections;
using UnityEngine;

public abstract class DialogueAction
{
    public abstract Coroutine Execute(object[] args);
    protected abstract IEnumerator ExecuteCoroutine(object[] args);
    protected abstract void CompleteAction();
    protected abstract void Validate(object[] args);
}
