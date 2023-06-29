using UnityEngine;

public static class TransformExtensions
{
    public static void SetEulerX(this Transform transform, float value)
    {
        var current = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(value, current.y, current.z);
    }

    public static void SetEulerY(this Transform transform, float value)
    {
        var current = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(current.x, value, current.z);
    }

    public static void SetEulerZ(this Transform transform, float value)
    {
        var current = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(current.x, current.y, value);
    }
}
