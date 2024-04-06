using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class TimeParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousTitle = context.CurrentContentStyle.Title;

            var title = htmlNode.GetAttributeValue<string>(name: "datetime", def: string.Empty);

            if (!string.IsNullOrWhiteSpace(title))
            {
                context.CurrentContentStyle.Title = title;
            }

            base.Parse(htmlNode, context);

            context.CurrentContentStyle.Title = previousTitle;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "time";
        }
    }
}
