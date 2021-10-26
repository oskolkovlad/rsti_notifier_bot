namespace RstiNotifierBot.Interfaces.Controllers.Parsers
{
    using System.Collections.Generic;

    internal interface IParserController<out T>
    {
        IEnumerable<T> Parse(string source);
    }
}
