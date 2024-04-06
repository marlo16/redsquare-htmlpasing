using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    internal class ParagraphParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var shouldAddRowForInnerContent = 
                (HasNotEmptyChildNode(htmlNode, context) || context.CurrentContentStyle.IsPreformattedText)
             && !context.IsCurrentRowEmpty;
            if (shouldAddRowForInnerContent)
            {
                AppendNewRow(context);
            }

            base.Parse(htmlNode, context);

            var nextNotEmptyNode = GetNextNotEmptyNode(htmlNode, context);
            var isExistsNextContent = nextNotEmptyNode != null 
                                  && !ShouldApply(nextNotEmptyNode, context);
            if (!isExistsNextContent || context.IsCurrentRowEmpty)
            {
                return;
            }

            AppendNewRow(context);
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "p";
        }
    }
}
