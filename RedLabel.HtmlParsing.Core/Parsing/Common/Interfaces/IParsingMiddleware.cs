using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public interface IParsingMiddleware
    {
        void Parse(HtmlNode node, ParsingContext context);
        bool ShouldApply(HtmlNode htmlNode, ParsingContext context);
        void SetParsingMediator(IParsingMediator parsingMediator);
    }
}
