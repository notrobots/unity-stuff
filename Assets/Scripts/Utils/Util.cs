public static class Util
{
    private static T ValueOrDefault<T>(T value, T defaultValue)
    {
        return value == null ? defaultValue : value;
    }

    
}
