using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingletonRegistry", menuName = "Core/Singleton Registry")]
public class SingletonRegistry : SingletonScriptableObject<SingletonRegistry>
{
    public List<ScriptableObject> list = new List<ScriptableObject>();

    public static T GetSingleton<T>()
        where T : SingletonScriptableObject<T>
    {
        return (T)Instance.list.Find(s => s.GetType() == typeof(T));
    }
}