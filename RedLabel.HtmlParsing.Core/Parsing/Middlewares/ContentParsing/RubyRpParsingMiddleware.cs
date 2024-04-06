using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class RubyRpParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            base.Parse(htmlNode, context);
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "rp";
        }
    }
}
