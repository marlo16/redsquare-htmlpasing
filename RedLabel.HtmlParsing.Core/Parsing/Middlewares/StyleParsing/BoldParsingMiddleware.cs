using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class BoldParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousValue = context.CurrentContentStyle.IsBold;
            context.CurrentContentStyle.IsBold = true;
            base.Parse(htmlNode, context);
            context.CurrentContentStyle.IsBold = previousValue;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            var nodeName = htmlNode.GetFormattedName();
            return nodeName == "b" || nodeName == "strong";
        }
    }
}
