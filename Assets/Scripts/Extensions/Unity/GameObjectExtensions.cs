using UnityEngine;

public static class GameObjectExtensions
{
    public static void SendMessageSafe(this GameObject gameObject, string methodName, object value)
    {
        gameObject.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
    }

    public static void SendMessageSafe(this GameObject gameObject, string methodName)
    {
        gameObject.SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
    }

    public static void SendMessageUpwardsSafe(this GameObject gameObject, string methodName, object value)
    {
        gameObject.SendMessageUpwards(methodName, value, SendMessageOptions.DontRequireReceiver);
    }

    public static void SendMessageUpwardsSafe(this GameObject gameObject, string methodName)
    {
        gameObject.SendMessageUpwards(methodName, SendMessageOptions.DontRequireReceiver);
    }

    public static void BroadcastMessageSafe(this GameObject gameObject, string methodName, object value)
    {
        gameObject.BroadcastMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
    }

    public static void BroadcastMessageSafe(this GameObject gameObject, string methodName)
    {
        gameObject.BroadcastMessage(methodName, SendMessageOptions.DontRequireReceiver);
    }
}
