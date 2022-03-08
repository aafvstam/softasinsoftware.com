namespace softasinsoftware.Shared.Extensions.System
{
    public static class StringExtensions
    {
        public static string Truncate(this string source, int length)
        {
            if (source.Length <= length)
            {
                return source;
            }

            return source[..Math.Min(length, source.Length)] + " ...";
        }
    }
}
