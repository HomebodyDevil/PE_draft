using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            if (gameObject == null)
            {
                Debug.Log("GameObject is Null");
            }
            
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
    }
}

public abstract class PersistantSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
