using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class ItalicParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousValue = context.CurrentContentStyle.IsItalic;
            context.CurrentContentStyle.IsItalic = true;
            base.Parse(htmlNode, context);
            context.CurrentContentStyle.IsItalic = previousValue;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            var nodeName = htmlNode.GetFormattedName();
            return nodeName == "i" || nodeName == "em" || nodeName == "cite" || nodeName == "dfn";
        }
    }
}
