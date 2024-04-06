using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class HtmlParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            base.Parse(htmlNode.NextSibling, context);
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "html";
        }
    }
}
