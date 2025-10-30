using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    public static T Instance { get; private set; }
   virtual protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this as T;//as 的作用 转化为指定的类型 
    }
    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
public abstract class PersistentSingleton<T>:Singleton<T> where T : MonoBehaviour
{
protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);//跨场景不销毁的
    }


}