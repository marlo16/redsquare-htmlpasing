using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    internal class BreakParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var isNextNodeEmpty = GetNextNotEmptyNode(htmlNode, context) is null;
            var isPreviousNodeEmpty = GetPreviousNotEmptyNode(htmlNode, context) is null;
            var isPreviousBreakNode = IsPreviousLineBreakNode(htmlNode, context);

            var shouldAppendRowBefore = (isPreviousBreakNode && !isNextNodeEmpty)
                                     || (isPreviousNodeEmpty && !isNextNodeEmpty);

            if (shouldAppendRowBefore && IsCurrentBlockEmpty(context))
            {
                AppendNewRow(context);
            }

            if (shouldAppendRowBefore || context.CurrentBlockElement != null
                                      && !context.CurrentBlockElement.Contents.Any())
            {
                var content = new BlockElementContent(content: string.Empty
                                        , context.CurrentContentStyle
                                        , context.CurrentBlockElement);
                context.CurrentBlockElement.Contents.Add(content);
            }

            base.Parse(htmlNode, context);

            var shouldAppendRowAfter = !isNextNodeEmpty 
                                   && (!shouldAppendRowBefore || IsCurrentBlockEmpty(context));
            if (shouldAppendRowAfter)
            {
                AppendNewRow(context);
            }
        }

        private bool IsCurrentBlockEmpty(ParsingContext context)
            => context.CurrentBlockElement is null
            || context.CurrentBlockElement.Contents.Any();

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode != null && (htmlNode.GetFormattedName() == "br" 
                                     || htmlNode.GetFormattedName() == "wbr");
        }

        private bool IsPreviousLineBreakNode(HtmlNode htmlNode, ParsingContext context)
        {
            if (htmlNode is null || !IsEmptyNode(htmlNode.PreviousSibling, context))
            {
                return false;
            }

            return ShouldApply(htmlNode.PreviousSibling, context) 
                || IsPreviousLineBreakNode(htmlNode.PreviousSibling, context);
        }      
    }
}
