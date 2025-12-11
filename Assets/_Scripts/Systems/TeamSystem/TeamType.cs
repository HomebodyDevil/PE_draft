using System;
using UnityEngine;

public enum Team
{
    Player,
    Friendly,
    Enemy,
}

public enum Classification
{
    Human,
}

[Serializable]
public class TeamType
{
    public int Team { get; private set; }
    public int Classification { get; private set; }
}
