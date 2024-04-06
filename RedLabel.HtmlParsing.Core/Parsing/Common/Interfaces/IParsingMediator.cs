using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;

namespace RedLabel.HtmlParsing.Core
{
    public interface IParsingMediator
    {
        Document Parse(HtmlDocument document);
        void Parse(HtmlNode node, ParsingContext context);
        void RegisterMiddleware(IParsingMiddleware parsingMiddleware);
    }
}
