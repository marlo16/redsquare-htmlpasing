using HtmlAgilityPack;
using System;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class FontParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousFontSize = context.CurrentContentStyle.FontSize;
            var previousFontFamilyName = context.CurrentContentStyle.FontFamilyName;
            var previousFontColor = context.CurrentContentStyle.FontColor;

            var fontSize = GetFontSize(htmlNode);
            if (fontSize.HasValue)
            {
                context.CurrentContentStyle.FontSize = fontSize.Value;
            }
            var fontFamilyName = GetFontFamilyName(htmlNode);
            if (!string.IsNullOrWhiteSpace(fontFamilyName))
            {
                context.CurrentContentStyle.FontFamilyName = fontFamilyName;
            }
            var fontColor = GetFontColor(htmlNode);
            if (!string.IsNullOrWhiteSpace(fontColor))
            {
                context.CurrentContentStyle.FontColor = fontColor;
            }

            base.Parse(htmlNode, context);

            context.CurrentContentStyle.FontSize = previousFontSize;
            context.CurrentContentStyle.FontFamilyName = previousFontFamilyName;
            context.CurrentContentStyle.FontColor = previousFontColor;
        }

        private float? GetFontSize(HtmlNode htmlNode)
        {
            var fontSizeAttribute = htmlNode.Attributes.FirstOrDefault(a
                => a.Name.Equals(value: "size", comparisonType: StringComparison.InvariantCulture))?.Value
                ?? string.Empty;
            var isFontSizeParsed = float.TryParse(fontSizeAttribute, out var fontSize);
            return isFontSizeParsed ? fontSize : (float?)null;
        }

        private string GetFontFamilyName(HtmlNode htmlNode)
        {
            var fontNameAttribute = htmlNode.Attributes.FirstOrDefault(a
                => a.Name.Equals(value: "face", comparisonType: StringComparison.InvariantCulture))?.Value
                ?? string.Empty;
            return fontNameAttribute;
        }

        private string GetFontColor(HtmlNode htmlNode)
        {
            var fontNameAttribute = htmlNode.Attributes.FirstOrDefault(a
                => a.Name.Equals(value: "color", comparisonType: StringComparison.InvariantCulture))?.Value
                ?? string.Empty;
            return fontNameAttribute;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "font";
        }
    }
}
