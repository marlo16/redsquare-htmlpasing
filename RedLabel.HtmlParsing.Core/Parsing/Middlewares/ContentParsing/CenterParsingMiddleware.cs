using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class CenterParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousHorizontalAlignment = context.CurrentRowStyle.HorizontalAlignment;
            context.CurrentRowStyle.HorizontalAlignment = HorizontalAlignment.Center;

            if (context.CurrentBlockElement is null || context.CurrentBlockElement.Contents.Any())
            {
                AppendNewRow(context);
            }
            else
            {
                context.CurrentBlockElement.ParentRow.RowStyle.HorizontalAlignment
                    = context.CurrentRowStyle.HorizontalAlignment;
            }

            base.Parse(htmlNode, context);

            context.CurrentRowStyle.HorizontalAlignment = previousHorizontalAlignment;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "center";
        }
    }
}
