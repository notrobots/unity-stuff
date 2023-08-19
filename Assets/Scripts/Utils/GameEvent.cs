using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game event utility that takes no parameters
/// </summary>
public static class GameEvent
{
    private static Dictionary<string, List<Listener>> listeners = new Dictionary<string, List<Listener>>();
    private static Dictionary<string, int> counters = new Dictionary<string, int>();
    public delegate void Listener();

    public static string CreateEventName(GameObject owner, string e)
    {
        return $"{owner.GetInstanceID()}.{e}";
    }

    public static void Register(GameObject owner, string e, Listener listener)
    {
        Register(CreateEventName(owner, e), listener);
    }

    public static void Register(string e, Listener listener)
    {
        if (!listeners.ContainsKey(e))
        {
            listeners[e] = new List<Listener>();
        }

        listeners[e].Add(listener);
    }

    public static void Unregister(GameObject owner, string e, Listener listener)
    {
        Unregister(CreateEventName(owner, e), listener);
    }

    public static void Unregister(string e, Listener listener)
    {
        if (listeners.ContainsKey(e))
        {
            listeners[e].Remove(listener);

            if (listeners[e].Count == 0)
            {
                listeners.Remove(e);
            }
        }
    }

    public static void UnregisterAll(GameObject owner, string e)
    {
        UnregisterAll(CreateEventName(owner, e));
    }

    public static void UnregisterAll(string e)
    {
        if (listeners.ContainsKey(e))
        {
            listeners[e].Clear();
            listeners.Remove(e);
        }
    }

    public static int Raise(GameObject target, string e)
    {
        return Raise($"{target.GetInstanceID()}.{e}");
    }

    public static int Raise(string e)
    {
        if (listeners.ContainsKey(e))
        {
            CountUp(e);
            listeners[e]?.ForEach(x => x?.Invoke());

            return listeners[e].Count;
        }

        return 0;
    }

    public static int RaiseDownwards(GameObject target, string e)
    {
        int receivers = Raise(target, e);

        foreach (Transform child in target.transform)
        {
            //TODO: Go deeper than one level
            receivers += Raise(child.gameObject, e);
        }

        return receivers;
    }

    public static int RaiseUpwards(GameObject target, string e)
    {
        int receivers = Raise(target, e);

        if (target.transform.parent != null)
        {
            receivers += Raise(target.transform.parent.gameObject, e);
        }

        return receivers;
    }

    private static void CountUp(string e)
    {
        if (counters.ContainsKey(e))
        {
            counters[e] += 1; //FIXME Not thread safe?
        }
    }

    public static void StopCount(string e)
    {
        if (!counters.ContainsKey(e))
        {
            throw new Exception($"Event <{e}> is not being counted. Did you forget to call GameEvent.Count(e)?");
        }

        counters.Remove(e);
    }

    public static void StopCount(GameObject owner, string e)
    {
        StopCount(CreateEventName(owner, e));
    }

    public static void Count(GameObject owner, string e)
    {
        Count(CreateEventName(owner, e));
    }

    public static void Count(string e)
    {
        if (!counters.ContainsKey(e))
        {
            counters.Add(e, 0);
        }
    }

    public static int GetCount(GameObject owner, string e)
    {
        return GetCount(CreateEventName(owner, e));
    }

    public static int GetCount(string e)
    {
        if (!counters.ContainsKey(e))
        {
            throw new Exception($"Event <{e}> is not being counted. Did you forget to call GameEvent.Count(e)?");
        }

        return counters[e];
    }

    public static void ResetCount(GameObject owner, string e)
    {
        ResetCount(CreateEventName(owner, e));
    }

    public static void ResetCount(string e)
    {
        if (!counters.ContainsKey(e))
        {
            throw new Exception($"Event <{e}> is not being counted. Did you forget to call GameEvent.Count(e)?");
        }

        counters[e] = 0;
    }
}

/// <summary>
/// Game event utility that takes one parameter
/// </summary>
public static class GameEvent<A>
{
    private static Dictionary<string, List<Listener>> listeners = new Dictionary<string, List<Listener>>();
    private static Dictionary<string, int> counters = new Dictionary<string, int>();
    public delegate void Listener(A a);

    public static string CreateEventName(GameObject owner, string e)
    {
        return $"{owner.GetInstanceID()}.{e}";
    }

    public static void Register(GameObject owner, string e, Listener listener)
    {
        Register(CreateEventName(owner, e), listener);
    }

    public static void Register(string e, Listener listener)
    {
        if (!listeners.ContainsKey(e))
        {
            listeners[e] = new List<Listener>();
        }

        listeners[e].Add(listener);
    }

    public static void Unregister(GameObject owner, string e, Listener listener)
    {
        Unregister(CreateEventName(owner, e), listener);
    }

    public static void Unregister(string e, Listener listener)
    {
        if (listeners.ContainsKey(e))
        {
            listeners[e].Remove(listener);

            if (listeners[e].Count == 0)
            {
                listeners.Remove(e);
            }
        }
    }

    public static void UnregisterAll(GameObject owner, string e)
    {
        UnregisterAll(CreateEventName(owner, e));
    }

    public static void UnregisterAll(string e)
    {
        if (listeners.ContainsKey(e))
        {
            listeners[e].Clear();
            listeners.Remove(e);
        }
    }

    public static int Raise(GameObject target, string e, A a = default)
    {
        return Raise($"{target.GetInstanceID()}.{e}", a);
    }

    public static int Raise(string e, A a = default)
    {
        if (listeners.ContainsKey(e))
        {
            CountUp(e);
            listeners[e]?.ForEach(x => x?.Invoke(a));

            return listeners[e].Count;
        }

        return 0;
    }

    public static int RaiseDownwards(GameObject target, string e, A a = default)
    {
        int receivers = Raise(target, e, a);

        foreach (Transform child in target.transform)
        {
            //TODO: Go deeper than one level
            receivers += Raise(child.gameObject, e, a);
        }

        return receivers;
    }

    public static int RaiseUpwards(GameObject target, string e, A a = default)
    {
        int receivers = Raise(target, e, a);

        if (target.transform.parent != null)
        {
            receivers += Raise(target.transform.parent.gameObject, e, a);
        }

        return receivers;
    }

    private static void CountUp(string e)
    {
        if (counters.ContainsKey(e))
        {
            counters[e] += 1; //FIXME Not thread safe?
        }
    }

    public static void StopCount(string e)
    {
        if (!counters.ContainsKey(e))
        {
            throw new Exception($"Event <{e}> is not being counted. Did you forget to call GameEvent.Count(e)?");
        }

        counters.Remove(e);
    }

    public static void StopCount(GameObject owner, string e)
    {
        StopCount(CreateEventName(owner, e));
    }

    public static void Count(GameObject owner, string e)
    {
        Count(CreateEventName(owner, e));
    }

    public static void Count(string e)
    {
        if (!counters.ContainsKey(e))
        {
            counters.Add(e, 0);
        }
    }

    public static int GetCount(GameObject owner, string e)
    {
        return GetCount(CreateEventName(owner, e));
    }

    public static int GetCount(string e)
    {
        if (!counters.ContainsKey(e))
        {
            throw new Exception($"Event <{e}> is not being counted. Did you forget to call GameEvent.Count(e)?");
        }

        return counters[e];
    }

    public static void ResetCount(GameObject owner, string e)
    {
        ResetCount(CreateEventName(owner, e));
    }

    public static void ResetCount(string e)
    {
        if (!counters.ContainsKey(e))
        {
            throw new Exception($"Event <{e}> is not being counted. Did you forget to call GameEvent.Count(e)?");
        }

        counters[e] = 0;
    }
}