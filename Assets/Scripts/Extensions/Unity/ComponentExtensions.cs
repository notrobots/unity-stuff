using UnityEngine;

public static class ComponentExtensions
{
    public static void SendMessageSafe(this Component component, string methodName, object value)
    {
        component.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
    }

    public static void SendMessageSafe(this Component component, string methodName)
    {
        component.SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
    }

    public static void SendMessageUpwardsSafe(this Component component, string methodName, object value)
    {
        component.SendMessageUpwards(methodName, value, SendMessageOptions.DontRequireReceiver);
    }

    public static void SendMessageUpwardsSafe(this Component component, string methodName)
    {
        component.SendMessageUpwards(methodName, SendMessageOptions.DontRequireReceiver);
    }

    public static void BroadcastMessageSafe(this Component component, string methodName, object value)
    {
        component.BroadcastMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
    }

    public static void BroadcastMessageSafe(this Component component, string methodName)
    {
        component.BroadcastMessage(methodName, SendMessageOptions.DontRequireReceiver);
    }
}
