using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Object = UnityEngine.Object;

public class DialogueActionContext
{
    public string FunctionName;
    public List<string> Parameters = new();
}

public abstract class DialogueAction
{
    public abstract Coroutine Execute(List<string> args);
    protected abstract IEnumerator ExecuteCoroutine();
    protected abstract void CompleteAction();
    protected abstract void Validate(List<string> args);
    protected abstract void StringToArgs(List<string> args);

    protected T StringToArg<T>(string arg)
    {
        Type type = typeof(T);

        if (type == typeof(int))
        {
            return (T)(object)int.Parse(arg, CultureInfo.InvariantCulture);
        }
        else if (type == typeof(float))
        {
            return (T)(object)float.Parse(arg, CultureInfo.InvariantCulture);
        }
        else if (type == typeof(string))
        {
            return (T)(object)arg;
        }
        else if (type == typeof(bool))
        {
            return (T)(object)bool.Parse(arg);
        }

        return (T)(object)null;
    }
}
