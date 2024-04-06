using HtmlAgilityPack;
using System;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class HyperlinkParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var hyperlink = htmlNode.Attributes.FirstOrDefault(a
                => a.Name.Equals(value: "href", comparisonType: StringComparison.InvariantCulture))?.Value
                ?? string.Empty;
            hyperlink = hyperlink.Trim();

            context.CurrentContentStyle.Hyperlink = hyperlink;

            base.Parse(htmlNode, context);

            context.CurrentContentStyle.Hyperlink = string.Empty;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "a";
        }
    }
}
