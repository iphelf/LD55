using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Component
{
    private static T instance;
    public static T Instance
    {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject newInstance = new GameObject();
                    instance = newInstance.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        instance = this as T;
    }
    //这段代码定义了一个名为 Singleton<T> 的单例类，其中 T 是 Component
    //类型或其子类型。这是一种常见的设计模式，用于确保一个类只有一个实例，并提供一个全局访问点。
}
