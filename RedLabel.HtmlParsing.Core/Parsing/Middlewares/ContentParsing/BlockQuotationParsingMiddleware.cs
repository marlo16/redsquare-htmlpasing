using HtmlAgilityPack;
using System;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{ 
    public class BlockQuotationParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousIndentationCoefficient = context.CurrentRowStyle.IndentationCoefficient;
            var previousTitle = context.CurrentContentStyle.Title;

            context.CurrentRowStyle.IndentationCoefficient++;
            var cite = htmlNode.GetAttributeValue<string>(name: "cite", def: string.Empty);
            if (!string.IsNullOrWhiteSpace(cite))
            {
                context.CurrentContentStyle.Title = cite;
            }

            if (context.CurrentBlockElement is null || context.CurrentBlockElement.Contents.Any())
            {
                AppendNewRow(context);
            }
            else
            {
                context.CurrentBlockElement.ParentRow.RowStyle.IndentationCoefficient 
                    = context.CurrentRowStyle.IndentationCoefficient;
            }

            base.Parse(htmlNode, context);

            context.CurrentRowStyle.IndentationCoefficient = previousIndentationCoefficient;
            context.CurrentContentStyle.Title = previousTitle;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "blockquote";
        }
    }
}
