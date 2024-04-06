using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class ThematicBreakParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            AppendNewRow(context);
            context.CurrentBlockElement.ParentRow.RowStyle.IsThematicBreakRow = true;

            base.Parse(htmlNode, context);

            var isNextNodeNotEmpty = GetNextNotEmptyNode(htmlNode, context) != null;
            if (isNextNodeNotEmpty)
            {
                AppendNewRow(context);
            }
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode != null && htmlNode.GetFormattedName() == "hr";
        }
    }
}
