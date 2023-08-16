public static class GenericExtensions
{
    public static T OrDefault<T>(this T value, T defaultValue)
    {
        return value == null ||
            (value is string && ((string)(object)value).Length == 0)
            ? defaultValue
            : value;
    }
}
