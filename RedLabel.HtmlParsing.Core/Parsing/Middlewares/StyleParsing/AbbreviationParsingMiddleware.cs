using HtmlAgilityPack;
using System;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class AbbreviationParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousTitle = context.CurrentContentStyle.Title;

            var title = htmlNode.Attributes.FirstOrDefault(a
                => a.Name.Equals(value: "title", comparisonType: StringComparison.InvariantCulture))?.Value 
                ?? string.Empty;
            title = title.Trim();

            context.CurrentContentStyle.Title = title;

            base.Parse(htmlNode, context);

            context.CurrentContentStyle.Title = previousTitle;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "abbr" 
                || htmlNode.GetFormattedName() == "acronym";
        }
    }
}
