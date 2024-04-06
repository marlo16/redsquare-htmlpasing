using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class HeadingsParsingMiddleware : BaseParsingMiddleware
    {
        private const float ONE_EM_IN_PIXELS = 16;

        private static readonly Dictionary<string, float> _headingSizesInEm
        = new Dictionary<string, float>
        {
            { "h1", 2f },
            { "h2", 1.5f },
            { "h3", 1.17f },
            { "h4", 1f },
            { "h5", 0.83f },
            { "h6", 0.67f },
        };

        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousFontSize = context.CurrentContentStyle.FontSize;

            var shouldAddRowForInnerContent = htmlNode.ChildNodes.Any(c => !IsEmptyNode(c, context))
                                           && !context.IsCurrentRowEmpty;
            if (shouldAddRowForInnerContent)
            {
                AppendNewRow(context);
            }

            var fontSizeInEm = _headingSizesInEm[htmlNode.GetFormattedName()];
            context.CurrentContentStyle.FontSize = fontSizeInEm * ONE_EM_IN_PIXELS;
            ParseChildNodes(htmlNode, context, previousFontSize);
            context.CurrentContentStyle.FontSize = previousFontSize;

            var nextNotEmptyNode = GetNextNotEmptyNode(htmlNode, context);
            var isExistsNextContent = nextNotEmptyNode != null
                                  && !ShouldApply(nextNotEmptyNode, context);
            if (!isExistsNextContent || context.IsCurrentRowEmpty 
              || HasInnerHeader(htmlNode, context))
            {
                return;
            }

            AppendNewRow(context);
        }

        private void ParseChildNodes(HtmlNode node, ParsingContext context, float previousFontSize)
        {
            if (node is null || node.ChildNodes is null)
            {
                return;
            }

            foreach (var childNode in node.ChildNodes)
            {
                if (ShouldApply(childNode, context))
                {
                    context.CurrentContentStyle.FontSize = previousFontSize;
                }

                this._parsingMediator.Parse(childNode, context);
            }
        }

        private bool HasInnerHeader(HtmlNode node, ParsingContext context)
        {
            if (node.ChildNodes is null || !node.ChildNodes.Any())
            {
                return false;
            }

            if (node.ChildNodes.Any(n => ShouldApply(n, context)))
            {
                return true;
            }

            return node.ChildNodes.Any(n => HasInnerHeader(n, context));
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return _headingSizesInEm.ContainsKey(htmlNode.GetFormattedName());
        }
    }
}
