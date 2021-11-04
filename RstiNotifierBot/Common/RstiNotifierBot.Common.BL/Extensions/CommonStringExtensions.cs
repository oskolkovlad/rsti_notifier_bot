namespace RstiNotifierBot.Common.BL.Extensions
{
    public static class CommonStringExtensions
    {
        public static string Clear(this string source, string value) => source?.Replace(value, string.Empty);
    }
}
