using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class AddressParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var isCurrentRowEmpty = context.IsCurrentRowEmpty;
            var hasAnyNextNotEmptyNode = GetNextNotEmptyNode(htmlNode, context) != null;
            var hasAnyChildNotEmptyNode = HasNotEmptyChildNode(htmlNode, context);

            if (hasAnyChildNotEmptyNode)
            {
                var previousIsItalic = context.CurrentContentStyle.IsItalic;
                if (!isCurrentRowEmpty)
                {
                    AppendNewRow(context);
                }
                context.CurrentContentStyle.IsItalic = true;
                base.Parse(htmlNode, context);
                context.CurrentContentStyle.IsItalic = previousIsItalic;
            }

            if (hasAnyNextNotEmptyNode && (hasAnyChildNotEmptyNode || !isCurrentRowEmpty))
            {
                AppendNewRow(context);
            }
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode != null && htmlNode.GetFormattedName() == "address";
        }
    }
}
