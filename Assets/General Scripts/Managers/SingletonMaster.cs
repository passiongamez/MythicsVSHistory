using Unity.VisualScripting;
using UnityEngine;

public class SingletonMaster<T> : MonoBehaviour where T: SingletonMaster<T> 
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if( _instance == null)
            {
                Debug.Log(typeof(T).ToString() + " is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = (T)this;
    }
}
