using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SingletonManager<T>: MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    GameObject singletonObj = new GameObject();
                    _instance = singletonObj.AddComponent<T>();
                    singletonObj.name = typeof(T).Name;
                    
                    DontDestroyOnLoad(singletonObj);
                    Debug.LogWarning($"{singletonObj.name} has been created.");
                }
                else
                {
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }
}