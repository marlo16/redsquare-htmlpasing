using RedLabel.HtmlParsing.Core.Models;

namespace RedLabel.HtmlParsing.Core.Tests
{
    public static class CreationRowHelper
    {
        public static TextStyle CreateTextStyle(
            bool isBold = default, bool isItalic = default, bool hasUnderLine = default
          , bool isStrikeout = default, float fontSize = 12f, string fontFamilyName = "Times New Roman"
          , string fontColor = "#000000", string title = "", string hyperlink = ""
          , TextDirectionType textDirectionType = TextDirectionType.LeftToRight
          , string backgroundColor = "#FFFFFF", bool isPreformattedText = false
          , bool isSuperscript = false, bool isSubscript = false)
        {
            return new TextStyle(isBold, isItalic, hasUnderLine, isStrikeout
                               , fontSize, fontFamilyName, fontColor, title, hyperlink
                               , textDirectionType, backgroundColor, isPreformattedText
                               , isSuperscript, isSubscript);
        }

        public static RowStyle CreateRowStyle(bool isThematicBreakRow = false
            , HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left
            , int indentationCoefficient = 0)
        {
            return new RowStyle(isThematicBreakRow, horizontalAlignment, indentationCoefficient);
        }

        public static BlockRow AppendRow(this Document document
                                       , RowStyle? rowStyle = null)
        {
            rowStyle ??= CreateRowStyle();
            var row = new BlockRow(document, rowStyle);
            document.Rows.Add(row);
            return row;
        }

        public static BlockRow AppendRow(this BlockElement blockElement
                                        , RowStyle? rowStyle = null)
        {
            rowStyle ??= CreateRowStyle();
            var document = blockElement.ParentRow.ParentDocument;
            return document.AppendRow(rowStyle);
        }

        public static BlockElement AppendBlock(this BlockRow row)
        {
            var blockElement = new BlockElement(row);
            row.Elements.Add(blockElement);
            return blockElement;
        }

        public static BlockElement AppendBlock(this BlockElement blockElement)
        {
            var row = blockElement.ParentRow;
            return row.AppendBlock();
        }

        public static BlockElement AppendContent(this BlockElement blockElement
            , string contentText, TextStyle? textStyle = null
            , bool isBarElementContent = false)
        {
            var contentTextStyle = textStyle is null 
                ? CreateTextStyle() 
                : new TextStyle(textStyle);
            var content = new BlockElementContent(contentText, contentTextStyle
                                                , blockElement, isBarElementContent);
            blockElement.Contents.Add(content);
            return blockElement;
        }
    }
}
