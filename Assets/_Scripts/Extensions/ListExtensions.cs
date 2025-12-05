using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int r = UnityEngine.Random.Range(0, list.Count);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }

    public static T GetRandom<T>(this List<T> list)
    {
        if (list.Count == 0) return default;
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
