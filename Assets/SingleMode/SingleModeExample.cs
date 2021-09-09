using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 单例模式 
/// 场景中有且仅有一个对象
/// 用于游戏中的单一功能管理器
/// 优点
/// 快速获取对象

//直接继承自泛型单例类来实现单例类
public class SingleModeExample : UnitySingleton<SingleModeExample> {

    public void Fun()
    {
        Debug.Log("SingleModeExample function");
    }
}


/// <summary>
/// 单例泛型类
/// </summary>
/// <typeparam name="T">泛型类型</typeparam>
public class UnitySingleton<T>:MonoBehaviour
    where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null) //从场景中获取对象
            {
                instance = FindObjectOfType(typeof(T))as T;
            }
            if(instance == null) //没有对象，初始化一个对象
            {
                GameObject obj = new GameObject();
                obj.AddComponent<T>();
                instance = obj.GetComponent<T>();
            }
            return instance; //返回对象
        }
    }

}