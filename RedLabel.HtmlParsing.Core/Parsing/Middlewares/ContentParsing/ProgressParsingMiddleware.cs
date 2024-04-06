using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;

namespace RedLabel.HtmlParsing.Core
{
    public class ProgressParsingMiddleware : BaseParsingMiddleware
    {
        private const float DEFAULT_MAX_VALUE = 1;
        private const float MAX_PERCENTAGE_VALUE = 100;
        private const float MIN_PERCENTAGE_VALUE = 0;
        private const string BACKGROUND_COLOR = "#0000ff";

        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var value = htmlNode.GetAttributeValue(name: "value", def: 0f);
            var maxValue = htmlNode.GetAttributeValue(name: "max", def: 0f);
            maxValue = maxValue == 0 ? DEFAULT_MAX_VALUE : maxValue;

            var percentage = (value / maxValue) * MAX_PERCENTAGE_VALUE;

            if (percentage < MIN_PERCENTAGE_VALUE)
            {
                percentage = MIN_PERCENTAGE_VALUE;
            }

            if (percentage > MAX_PERCENTAGE_VALUE)
            {
                percentage = MAX_PERCENTAGE_VALUE;
            }

            if (context.CurrentBlockElement is null)
            {
                AppendNewRow(context);
            }

            var previousBackgroundColor = context.CurrentContentStyle.BackgroundColor;
            context.CurrentContentStyle.BackgroundColor = BACKGROUND_COLOR;

            var content = new BlockElementContent(percentage.ToString()
                , context.CurrentContentStyle, context.CurrentBlockElement
                , isBarElementContent: true);
            context.CurrentBlockElement.Contents.Add(content);

            context.CurrentContentStyle.BackgroundColor = previousBackgroundColor;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            var nodeName = htmlNode.GetFormattedName();
            return nodeName == "progress";
        }
    }
}
