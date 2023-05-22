public static class NumberExtensions
{
    public static double IncreasedBy(this double value, double percentage)
    {
        return value + percentage.PercentOf(value);
    }

    public static float IncreasedBy(this float value, float percentage)
    {
        return value + percentage.PercentOf(value);
    }

    public static double DecreasedBy(this double value, double percentage)
    {
        return value - percentage.PercentOf(value);
    }

    public static float DecreasedBy(this float value, float percentage)
    {
        return value - percentage.PercentOf(value);
    }

    public static double PercentOf(this double percentage, double number)
    {
        return percentage / 100 * number;
    }

    public static float PercentOf(this float percentage, float number)
    {
        return percentage / 100 * number;
    }
}