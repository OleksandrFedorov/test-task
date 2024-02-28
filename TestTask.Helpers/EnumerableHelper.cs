namespace TestTask.Helpers
{
    public static class EnumerableHelper
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var e in source)
            {
                action(e);
                yield return e;
            }
        }
    }
}
