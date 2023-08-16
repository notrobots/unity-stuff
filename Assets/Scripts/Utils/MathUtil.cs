public static class MathUtil
{
    public static double Average(int startInclusive, int endInclusive)
    {
        double sum = 0;
        double count = 0;

        for (int i = startInclusive; i < endInclusive + 1; i++)
        {
            sum += i;
            count++;
        }

        return sum / count;
    }
}