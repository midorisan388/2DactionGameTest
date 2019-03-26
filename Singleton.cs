﻿using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton _instance = (Singleton)null;

    private static Singleton instance
    {
        get
        {
            return Singleton._instance ?? (Singleton._instance = Object.FindObjectOfType<Singleton>());
        }
    }

    private void Awake()
    {
        if ((Object)this != (Object)Singleton.instance)
            Object.Destroy((Object)this.gameObject);
        else
            Object.DontDestroyOnLoad((Object)this.gameObject);
    }

    private void OnDestroy()
    {
        if (!((Object)this == (Object)Singleton.instance))
            return;
        Singleton._instance = (Singleton)null;
    }
}
