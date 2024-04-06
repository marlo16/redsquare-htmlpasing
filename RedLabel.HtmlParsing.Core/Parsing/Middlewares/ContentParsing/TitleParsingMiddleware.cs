using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;

namespace RedLabel.HtmlParsing.Core
{
    public class TitleParsingMiddleware : BaseParsingMiddleware
    {
        private const string REPLACE_PATTERN = @"\s+";
        private const string SINGLE_WHITESPACE = " ";

        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var title = htmlNode.InnerHtml;
            title = title?.Replace(oldValue: Environment.NewLine, newValue: " ")?.Trim();
            title = Regex.Replace(title, REPLACE_PATTERN, SINGLE_WHITESPACE);

            if (string.IsNullOrWhiteSpace(context.CurrentDocument.Title) 
            && !string.IsNullOrWhiteSpace(title))
            {
                context.CurrentDocument.Title = title;
            }
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "title";
        }
    }
}
