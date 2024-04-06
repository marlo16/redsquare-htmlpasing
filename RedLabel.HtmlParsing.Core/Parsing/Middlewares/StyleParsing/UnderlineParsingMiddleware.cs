using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class UnderlineParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousValue = context.CurrentContentStyle.HasUnderLine;
            context.CurrentContentStyle.HasUnderLine = true;
            base.Parse(htmlNode, context);
            context.CurrentContentStyle.HasUnderLine = previousValue;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            var nodeName = htmlNode.GetFormattedName();
            return nodeName == "u" || nodeName == "ins";
        }
    }
}
