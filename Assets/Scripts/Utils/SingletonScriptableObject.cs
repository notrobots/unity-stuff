using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject
    where T : SingletonScriptableObject<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
#if UNITY_EDITOR
            if (_instance == null)
            {
                var assets = AssetDatabase.FindAssets($"t: {typeof(T).Name}")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>);

                if (assets != null && assets.Count() > 0)
                {
                    _instance = assets.ElementAt(0);
                }

                if (_instance != null)
                {
                    Debug.Log($"{typeof(T)} was loaded from the AssetDatabase");
                }
            }
#endif

            if (_instance == null)
            {
                var assets = Resources.LoadAll<T>("");

                if (assets != null && assets.Length > 0)
                {
                    _instance = assets[0];
                }

                if (_instance != null)
                {
                    Debug.Log($"{typeof(T)} was loaded from the Resources");
                }
            }

            if (_instance == null)
            {
                _instance = SingletonRegistry.GetSingleton<T>();

                if (_instance != null)
                {
                    Debug.Log($"{typeof(T)} was loaded from the Singleton Registry");
                }
            }

            if (_instance == null)
            {
                _instance = CreateInstance<T>();


                if (_instance != null)
                {
                    Debug.Log($"{typeof(T)} was initialized. All values will be zeroed");
                }
            }

            _instance.OnInit();

            return _instance;
        }
    }

    protected virtual void OnInit() { }
}
