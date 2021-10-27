namespace RstiNotifierBot.Extensions
{
    internal static class StringExtensions
    {
        public static string Clear(this string rr, string value) => rr.Replace(value, string.Empty);
    }
}
