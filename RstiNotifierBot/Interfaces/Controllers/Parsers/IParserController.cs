namespace RstiNotifierBot.Interfaces.Controllers.Parsers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal interface IParserController<T>
    {
        Task<IEnumerable<T>> ParseAsync(string source);
    }
}
