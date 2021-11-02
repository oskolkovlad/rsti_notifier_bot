namespace RstiNotifierBot.BL.Interfaces.Parsers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IParserController<T>
    {
        Task<IEnumerable<T>> ParseAsync(string source);
    }
}
