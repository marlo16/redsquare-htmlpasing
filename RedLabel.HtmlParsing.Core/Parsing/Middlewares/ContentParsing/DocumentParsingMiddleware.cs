using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;

namespace RedLabel.HtmlParsing.Core
{
    public class DocumentParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            var contentStyle = new TextStyle
                (isBold: false, isItalic: false, hasUnderLine: false
               , isStrikeout: false, fontSize: 12, fontFamilyName: "Times New Roman"
               , fontColor: "#000000", title: string.Empty, hyperlink: string.Empty
               , textDirectionType: TextDirectionType.LeftToRight, backgroundColor: "#FFFFFF"
               , isPreformattedText: false, isSuperscript: false, isSubrscript: false);
            context.SetContentStyle(contentStyle);

            var rowStyle = new RowStyle(isThematicBreakRow: false
                , horizontalAlignment: HorizontalAlignment.Left, indentationCoefficient: 0);
            context.SetRowStyle(rowStyle);

            base.Parse(htmlNode, context);
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "#document";
        }
    }
}
