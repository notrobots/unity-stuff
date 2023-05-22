using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceBehaviour : MonoBehaviour
{
    private Dictionary<Type, Component> componentCache = new Dictionary<Type, Component>();

    public new Renderer renderer => GetComponentOrCache<Renderer>();
    public SpriteRenderer spriteRenderer => GetComponentOrCache<SpriteRenderer>();
    public Animator animator => GetComponentOrCache<Animator>();
    public new Rigidbody rigidbody => GetComponentOrCache<Rigidbody>();
    public new Rigidbody2D rigidbody2D => GetComponentOrCache<Rigidbody2D>();
    public AudioSource audioSource => GetComponentOrCache<AudioSource>();

    public bool HasRenderer(bool cache = true) => HasComponent<Renderer>(cache);
    public bool HasSpriteRenderer(bool cache = true) => HasComponent<SpriteRenderer>(cache);
    public bool HasAnimator(bool cache = true) => HasComponent<Animator>(cache);
    public bool HasRigidbody(bool cache = true) => HasComponent<Rigidbody>(cache);
    public bool HasRigidbody2D(bool cache = true) => HasComponent<Rigidbody2D>(cache);
    public bool HasAudioSource(bool cache = true) => HasComponent<AudioSource>(cache);

    /// <summary>
    /// Returns the component or the cached value if available
    /// </summary>
    public C GetComponentOrCache<C>()
        where C : Component
    {
        var type = typeof(C);
        var component = componentCache.GetValueOrDefault(type, null);

        if (component == null)
        {
            component = GetComponent<C>();
            componentCache[type] = component;
        }

        return (C)component;
    }

    /// <summary>
    /// Returns whether or not this gameObject has the given componenet.
    /// 
    /// If cache is true it will cache the component.
    /// </summary>
    public bool HasComponent<C>(bool cache = true)
        where C : Component
    {
        return cache ? GetComponentOrCache<C>() : GetComponent<C>();
    }

    /// <summary>
    /// Clears the component cache
    /// </summary>
    public void ClearComponentsCache()
    {
        componentCache.Clear();
    }

    /// <summary>
    /// Clears the cache of the given component
    /// </summary>
    public void ClearComponentCache<C>()
        where C : Component
    {
        var type = typeof(C);

        if (componentCache.ContainsKey(type))
        {
            componentCache.Remove(type);
        }
    }

    /// <summary>
    /// Loads the scene at the given scene build index
    /// </summary>
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Loads the scene with the given name
    /// </summary>
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
