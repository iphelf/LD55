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
    //��δ��붨����һ����Ϊ Singleton<T> �ĵ����࣬���� T �� Component
    //���ͻ��������͡�����һ�ֳ��������ģʽ������ȷ��һ����ֻ��һ��ʵ�������ṩһ��ȫ�ַ��ʵ㡣
}
