namespace RstiNotifierBot.Common.BL.Extensions
{
    using System;

    public static class ConsoleHelper
    {
        private const int Width = 100;

        private static readonly string Line = new string('-', 50);
        private static readonly string LineBold = new string('=', 50);

        #region Public Members

        public static void OutputConsoleMessage(
            string message,
            bool isBold = false,
            bool withStartSeparator = true,
            bool withFinishSeparator = true,
            bool newLineAfter = false)
        {
            Action setSeparator = () =>
            {
                if (isBold)
                {
                    SetBoldSeparator();
                }
                else
                {
                    SetSeparator();
                }
            };

            if (withStartSeparator)
            {
                setSeparator();
            }
            
            Console.WriteLine(message);

            if (withFinishSeparator)
            {
                setSeparator();
            }

            if (newLineAfter)
            {
                Console.WriteLine();
            }
        }

        public static void OutputNowDateTime(
            bool isBold = false,
            bool withStartSeparator = true,
            bool withFinishSeparator = true,
            bool newLineAfter = false)
        {
            var dateTime = $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}";
            ConsoleHelper.OutputConsoleMessage(dateTime, withFinishSeparator: false, newLineAfter: true);
        }

        public static void OutputConsoleLog(this Exception exception)
        {
            SetBoldSeparator();

            Console.WriteLine(exception.Message);
            var innerException = exception.InnerException;
            if (innerException != null)
            {
                Console.WriteLine(innerException);
            }

            var stackTrace = exception.StackTrace;
            if (!string.IsNullOrEmpty(stackTrace))
            {
                SetSeparator();
                Console.WriteLine(exception.StackTrace);
            }

            SetBoldSeparator(true);
        }

        #endregion

        #region Private Members

        private static void SetSeparator(bool withNewLine = false) =>
            Console.WriteLine(withNewLine ? GetSeparatorWithNewLine(Line) : Line);

        private static void SetBoldSeparator(bool withNewLine = false) =>
            Console.WriteLine(withNewLine ? GetSeparatorWithNewLine(LineBold) : LineBold);

        private static string GetSeparatorWithNewLine(string separator) =>
            string.Concat(separator, Environment.NewLine);

        #endregion
    }
}
