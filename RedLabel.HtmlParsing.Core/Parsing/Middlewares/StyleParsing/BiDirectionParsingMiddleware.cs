using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;
using System;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class BiDirectionParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var textDirectionType = GetTextDirection(htmlNode);
            var previousTextDirectionType = context.CurrentContentStyle.TextDirectionType;

            context.CurrentContentStyle.TextDirectionType = textDirectionType;

            ParseChilds(htmlNode, context);

            context.CurrentContentStyle.TextDirectionType = previousTextDirectionType;
        }

        private TextDirectionType GetTextDirection(HtmlNode htmlNode)
        {
            var directionAttribute = htmlNode.Attributes?.FirstOrDefault(a
                => a.Name.Equals(value: "dir", comparisonType: StringComparison.InvariantCulture))?.Value
                ?? string.Empty;
            return directionAttribute.Equals(value: "rtl")
                ? TextDirectionType.RightToLeft
                : TextDirectionType.LeftToRight;
        }

        private void ParseChilds(HtmlNode node, ParsingContext context)
        {
            if (node is null || node.ChildNodes is null)
            {
                return;
            }

            foreach (var childNode in node.ChildNodes)
            {
                _parsingMediator.Parse(childNode, context);
            }
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "bdo";
        }
    }
}
