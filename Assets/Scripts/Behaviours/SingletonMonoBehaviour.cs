using UnityEngine;

/// <summary>
/// Singleton MonoBehaviour.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
    where T : SingletonMonoBehaviour<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else
        {
            Debug.LogError($"Trying to create an instance of an already instantiated singleton class {typeof(T)}");
            DestroyImmediate(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
