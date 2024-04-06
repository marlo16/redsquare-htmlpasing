using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class SuperscriptParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousSubscriptStyle = context.CurrentContentStyle.IsSubrscript;
            var previousSuperscriptStyle = context.CurrentContentStyle.IsSuperscript;

            context.CurrentContentStyle.IsSuperscript = true;
            context.CurrentContentStyle.IsSubrscript = false;
            base.Parse(htmlNode, context);

            context.CurrentContentStyle.IsSuperscript = previousSuperscriptStyle;
            context.CurrentContentStyle.IsSubrscript = previousSubscriptStyle;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "sup";
        }
    }
}
