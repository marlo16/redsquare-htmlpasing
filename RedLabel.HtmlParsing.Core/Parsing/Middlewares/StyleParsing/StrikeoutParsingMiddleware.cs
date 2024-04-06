using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class StrikeoutParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousValue = context.CurrentContentStyle.IsStrikeout;
            context.CurrentContentStyle.IsStrikeout = true;
            base.Parse(htmlNode, context);
            context.CurrentContentStyle.IsStrikeout = previousValue;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            var nodeName = htmlNode.GetFormattedName();
            return nodeName == "s" || nodeName == "del" || nodeName == "strike";
        }
    }
}
