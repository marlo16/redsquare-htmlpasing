using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class PreformattedParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            if (context.CurrentBlockElement is null)
            {
                AppendNewRow(context);
            }

            var previousPreformattedValue = context.CurrentContentStyle.IsPreformattedText;

            context.CurrentContentStyle.IsPreformattedText = true;
            base.Parse(htmlNode, context);

            context.CurrentContentStyle.IsPreformattedText = previousPreformattedValue;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "pre";
        }
    }
}
