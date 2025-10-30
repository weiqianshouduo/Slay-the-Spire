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
        Instance = this as T;//as ������ ת��Ϊָ�������� 
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
        DontDestroyOnLoad(gameObject);//�糡�������ٵ�
    }


}