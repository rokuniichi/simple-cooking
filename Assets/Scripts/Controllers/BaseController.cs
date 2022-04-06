using UnityEngine;

public abstract class BaseController<T> : MonoBehaviour where T : BaseController<T>
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}