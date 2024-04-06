using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;

namespace RedLabel.HtmlParsing.Core
{
    public class ShortQuotationParsingMiddleware : BaseParsingMiddleware
    {
        private const string OPEN_QUOTATION_SYMBOL = "\"";
        private const string CLOSE_QUOTATION_SYMBOL = "\"";

        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            if (context.CurrentBlockElement is null)
            {
                AppendNewRow(context);
            }

            var openQuotationContent = new BlockElementContent
                (OPEN_QUOTATION_SYMBOL, context.CurrentContentStyle, context.CurrentBlockElement);
            context.CurrentBlockElement.Contents.Add(openQuotationContent);

            base.Parse(htmlNode, context);

            var closeQuotationContent = new BlockElementContent
                (CLOSE_QUOTATION_SYMBOL, context.CurrentContentStyle, context.CurrentBlockElement);
            context.CurrentBlockElement.Contents.Add(closeQuotationContent);
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "q";
        }
    }
}
