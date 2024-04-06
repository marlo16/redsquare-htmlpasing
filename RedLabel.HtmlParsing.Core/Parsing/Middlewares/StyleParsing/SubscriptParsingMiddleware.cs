using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class SubscriptParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousSubscriptStyle = context.CurrentContentStyle.IsSubrscript;
            var previousSuperscriptStyle = context.CurrentContentStyle.IsSuperscript;

            context.CurrentContentStyle.IsSuperscript = false;
            context.CurrentContentStyle.IsSubrscript = true;
            base.Parse(htmlNode, context);

            context.CurrentContentStyle.IsSuperscript = previousSuperscriptStyle;
            context.CurrentContentStyle.IsSubrscript = previousSubscriptStyle;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "sub";
        }
    }
}
