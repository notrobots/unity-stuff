using System;
using System.Collections.Generic;

public static class EnumUtil
{
    public static IEnumerable<E> ExtractFlags<E>(E value)
        where E : Enum
    {
        foreach (E enumValue in Enum.GetValues(typeof(E)))
        {
            if (value.HasFlag(enumValue))
            {
                yield return enumValue;
            }
        }
    }

    public static void AddFlag<E>(ref E flags, E flag)
        where E : Enum
    {
        flags = (E)(object)((int)(object)flags | (int)(object)flag);
    }

    public static void RemoveFlag<E>(ref E flags, E flag)
        where E : Enum
    {
        flags = (E)(object)((int)(object)flags & (~(int)(object)flag));
    }
}
