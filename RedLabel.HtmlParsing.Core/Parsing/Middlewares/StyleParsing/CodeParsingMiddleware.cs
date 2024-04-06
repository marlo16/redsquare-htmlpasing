using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public class CodeParsingMiddleware : BaseParsingMiddleware
    {
        private const string CODE_FONT_FAMILY_NAME = "Consolas";

        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var previousFontFamily = context.CurrentContentStyle.FontFamilyName;

            context.CurrentContentStyle.FontFamilyName = CODE_FONT_FAMILY_NAME;
            base.Parse(htmlNode, context);

            context.CurrentContentStyle.FontFamilyName = previousFontFamily;
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            var nodeName = htmlNode.GetFormattedName();
            return nodeName == "code" || nodeName == "samp" 
                || nodeName == "kbd" || nodeName == "var" 
                || nodeName == "pre" || nodeName == "tt";
        }
    }
}
