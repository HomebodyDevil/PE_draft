using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameFlags
{
    private readonly HashSet<string> _flags = new();

    public bool Has(string key) => _flags.Contains(key);
    public void Set(string key) => _flags.Add(key);
    public void Clear() => _flags.Clear();

    public GameFlags() { }
    
    public GameFlags(GameFlags other)
    {
        if (other == null)
        {
            Debug.Log("GameFlags is null");
            return;
        }

        _flags = new HashSet<string>(other._flags);
    }

    public GameFlags(List<string> flags)
    {
        foreach (var flag in flags)
        {
            if (string.IsNullOrEmpty(flag)) continue;
            _flags.Add(flag);
        }
    }
}
