namespace RstiNotifierBot.Extensions
{
    using System;

    internal static class ExceptionExtensions
    {
        public static void OutputConsoleLog(this Exception exception)
        {
            Console.WriteLine(exception.Message);
            var innerException = exception.InnerException;
            if (innerException != null)
            {
                Console.WriteLine(innerException);
            }

            var stackTrace = exception.StackTrace;
            if (!string.IsNullOrEmpty(stackTrace))
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine(exception.StackTrace);
            }
            
            Console.WriteLine(string.Concat(new string('=', 50), Environment.NewLine));
        }
    }
}
