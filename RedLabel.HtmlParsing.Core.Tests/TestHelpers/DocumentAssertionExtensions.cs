using FluentAssertions;
using RedLabel.HtmlParsing.Core.Models;

namespace RedLabel.HtmlParsing.Core.Tests.Extensions
{
    public static class DocumentAssertionExtensions
    {
        public static void AssertThatEqual(this Document document
                                         , Document comparingDocument)
        {
            document.Title.Should().Be(comparingDocument.Title);
            document.Rows.Should().HaveCount(comparingDocument.Rows.Count);
            for (var i = 0; i < comparingDocument.Rows.Count; i++)
            {
                document.Rows[i].AssertThatEqual(comparingDocument.Rows[i]);
            }
        }

        public static void AssertThatEqual(this BlockRow blockRow
                                         , BlockRow comparingBlockRow)
        {
            blockRow.RowStyle.AssertThatEqual(comparingBlockRow.RowStyle);
            blockRow.Elements.Should().HaveCount(comparingBlockRow.Elements.Count);
            for (var i = 0; i < blockRow.Elements.Count; i++)
            {
                blockRow.Elements[i].AssertThatEqual(comparingBlockRow.Elements[i]);
            }
        }

        public static void AssertThatEqual(this RowStyle rowStyle, RowStyle comparingRowStyle)
        {
            rowStyle.IsThematicBreakRow.Should().Be(comparingRowStyle.IsThematicBreakRow);
            rowStyle.HorizontalAlignment.Should().Be(comparingRowStyle.HorizontalAlignment);
            rowStyle.IndentationCoefficient.Should().Be(comparingRowStyle.IndentationCoefficient);
        }

        public static void AssertThatEqual(this BlockElement blockElement
                                         , BlockElement comparingBlockElement)
        {
            blockElement.Contents.Should().HaveCount(comparingBlockElement.Contents.Count);
            for (var i = 0; i < blockElement.Contents.Count; i++)
            {
                blockElement.Contents[i].AssertThatEqual(comparingBlockElement.Contents[i]);
            }
        }

        public static void AssertThatEqual(this BlockElementContent blockElementContent
                                         , BlockElementContent comparingBlockElementContent)
        {
            blockElementContent.Content.Should().BeEquivalentTo(comparingBlockElementContent.Content);
            blockElementContent.IsBarElementContent.Should().Be(comparingBlockElementContent.IsBarElementContent);
            blockElementContent.ContentStyle.AssertThatEqual(comparingBlockElementContent.ContentStyle);
        }

        public static void AssertThatEqual(this TextStyle textStyle
                                         , TextStyle comparingTextStyle)
        {
            textStyle.IsBold.Should().Be(comparingTextStyle.IsBold);
            textStyle.IsItalic.Should().Be(comparingTextStyle.IsItalic);
            textStyle.IsStrikeout.Should().Be(comparingTextStyle.IsStrikeout);
            textStyle.HasUnderLine.Should().Be(comparingTextStyle.HasUnderLine);
            textStyle.FontFamilyName.Should().Be(comparingTextStyle.FontFamilyName);
            textStyle.FontColor.Should().Be(comparingTextStyle.FontColor);
            textStyle.FontSize.Should().Be(comparingTextStyle.FontSize);
            textStyle.Title.Should().Be(comparingTextStyle.Title);
            textStyle.Hyperlink.Should().Be(comparingTextStyle.Hyperlink);
            textStyle.IsHyperlink.Should().Be(comparingTextStyle.IsHyperlink);
            textStyle.TextDirectionType.Should().Be(comparingTextStyle.TextDirectionType);
            textStyle.BackgroundColor.Should().Be(comparingTextStyle.BackgroundColor);
            textStyle.IsPreformattedText.Should().Be(comparingTextStyle.IsPreformattedText);
            textStyle.IsSuperscript.Should().Be(comparingTextStyle.IsSuperscript);
            textStyle.IsSubrscript.Should().Be(comparingTextStyle.IsSubrscript);
        }
    }
}
