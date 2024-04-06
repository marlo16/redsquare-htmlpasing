using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class RubyParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            base.Parse(htmlNode, context);
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "ruby";
        }
    }
}
