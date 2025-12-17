using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class GameAbility
{
    public PEEnum.GAExecutor Executor { get; private set; }

    public virtual void SetExecutor(PEEnum.GAExecutor executorType)
    {
        this.Executor = executorType;
    }
}
