using System;
using System.Collections.Generic;

/// <summary>
/// <para>Game event utility</para>
/// <para>The event names should follow the following format: Who.What.Variation</para>
/// <para>Where 'Who' is the entity raising the event.</para>
/// <para>'What' is a description of what's happening and 'Variation' is a variation of the event.</para>
/// <para>E.g.</para>
/// <list type="bullet">
/// <item>Player.Fire</item>
/// <item>Player.GainHealth</item>
/// <item>Player.LoseHealth</item>
/// <item>Player.CollideEnemy</item>
/// <item>Player.HitEnemy</item>
/// </list>
/// </summary>
public static class GameEvent
{
    private static Dictionary<string, Listener> listeners = new Dictionary<string, Listener>();
    private static Dictionary<string, int> counters = new Dictionary<string, int>();

    public delegate void Listener(object? value);
    public delegate void EmptyListener();

    public static void Register(string e, EmptyListener listener)
    {
        Register(e, () => { listener(); }); //TODO: Is this good performance-wise?
    }

    public static void Register(string e, Listener listener)
    {
        if (!listeners.ContainsKey(e))
        {
            listeners.Add(e, listener);
            // listeners.Add(e, (v) => { (T)v });
        }
        else
        {
            listeners[e] += listener;
        }
    }

    public static void Unregister(string e, Listener listener)
    {
        if (listeners.ContainsKey(e))
        {
            listeners[e] -= listener;
        }
    }

    public static void UnregisterAll(string e)
    {
        if (listeners.ContainsKey(e))
        {
            listeners[e] = null;
        }
    }

    public static void Raise(string e, object value = null)
    {
        if (listeners.ContainsKey(e))
        {
            CountUp(e);
            listeners[e]?.Invoke(value);
        }
    }

    private static void CountUp(string e)
    {
        if (counters.ContainsKey(e))    //FIXME Not thread safe
        {
            counters[e] += 1;
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

    public static void Count(string e)
    {
        if (!counters.ContainsKey(e))
        {
            counters.Add(e, 0);
        }
    }

    public static int GetCount(string e)
    {
        if (counters.ContainsKey(e))
        {
            return counters[e];
        }

        throw new Exception($"Event <{e}> is not being counted. Did you forget to call GameEvent.Count(e)?");
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