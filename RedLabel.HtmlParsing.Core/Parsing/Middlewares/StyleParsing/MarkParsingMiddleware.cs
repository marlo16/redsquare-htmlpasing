using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class MarkParsingMiddleware : BaseParsingMiddleware
    {
        private const string YELLOW_BACKGOUND_COLOR = "#FFFF00";

        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousBackgroundColor = context.CurrentContentStyle.BackgroundColor;

            context.CurrentContentStyle.BackgroundColor = YELLOW_BACKGOUND_COLOR;
            base.Parse(htmlNode, context);

            context.CurrentContentStyle.BackgroundColor = previousBackgroundColor;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            var nodeName = htmlNode.GetFormattedName();
            return nodeName == "mark";
        }
    }
}
