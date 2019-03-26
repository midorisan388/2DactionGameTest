﻿using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object _lock = new object();
    private static bool applicationIsQuitting = false;
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (Singleton<T>.applicationIsQuitting)
            {
                Debug.LogWarning((object)("[Singleton] Instance '" + (object)typeof(T) + "' already destroyed on application quit. Won't create again - returning null."));
                return (T)null;
            }
            lock (Singleton<T>._lock)
            {
                if ((Object)Singleton<T>._instance == (Object)null)
                {
                    Singleton<T>._instance = (T)Object.FindObjectOfType(typeof(T));
                    if (Object.FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError((object)"[Singleton] Something went really wrong  - there should never be more than 1 singleton! Reopenning the scene might fix it.");
                        return Singleton<T>._instance;
                    }
                    if ((Object)Singleton<T>._instance == (Object)null)
                    {
                        GameObject gameObject = new GameObject();
                        Singleton<T>._instance = gameObject.AddComponent<T>();
                        gameObject.name = typeof(T).ToString() + " (Singleton)";
                        Object.DontDestroyOnLoad((Object)gameObject);
                        Debug.Log((object)("[Singleton] An instance of " + (object)typeof(T) + " is needed in the scene, so '" + (object)gameObject + "' was created with DontDestroyOnLoad."));
                    }
                    else
                        Debug.Log((object)("[Singleton] Using instance already created: " + Singleton<T>._instance.gameObject.name));
                }
                return Singleton<T>._instance;
            }
        }
    }

    public void OnDestroy()
    {
        Singleton<T>.applicationIsQuitting = true;
    }
}

