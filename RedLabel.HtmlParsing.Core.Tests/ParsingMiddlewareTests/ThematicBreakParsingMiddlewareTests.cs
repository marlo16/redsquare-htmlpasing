using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class ThematicBreakParsingMiddlewareTests
    {
        [TestCase("<p> tenst 1324 </p> <hr>", "tenst 1324")]
        [TestCase("tenst 1324  <hr>", "tenst 1324")]
        [TestCase(" 123123 <br> <hr>", "123123")]
        [TestCase("<p> tenst 1324  <hr> </p>", "tenst 1324")]
        public void ParseThematicBreak_SingleThematicBreak_ReturnOneRowAndOneWithBreak
            (string html, string expectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var rowStyle = CreationRowHelper.CreateRowStyle(isThematicBreakRow: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(expectedContent)
                .AppendRow(rowStyle).AppendBlock()
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<p> tenst 1324 </p> <hr> <hr>", "tenst 1324")]
        [TestCase("tenst 1324  <hr>  <hr>", "tenst 1324")]
        [TestCase(" 123123 <br> <hr> <hr>", "123123")]
        [TestCase("<p> tenst 1324  <hr>  <hr></p>", "tenst 1324")]
        public void ParseThematicBreak_TwoThematicBreaks_ReturnOneRowAndTwoWithBreak
            (string html, string expectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var rowStyle = CreationRowHelper.CreateRowStyle(isThematicBreakRow: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(expectedContent)
                .AppendRow(rowStyle).AppendBlock()
                .AppendRow(rowStyle).AppendBlock()
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<hr> <hr>")]
        [TestCase("<br> <hr> <hr>")]
        [TestCase("<br> <br> <hr> <hr>")]
        [TestCase("<p> <br> </p> <hr> <hr>")]
        public void ParseThematicBreak_OnlyTwoThematicBreaks_ReturnTwoRowsWithBreak(string html)
        {
            // Arrange
            var sut = CreateSut();
            var rowStyle = CreationRowHelper.CreateRowStyle(isThematicBreakRow: true);
            var expectedDocument = new Document()
                .AppendRow(rowStyle).AppendBlock()
                .AppendRow(rowStyle).AppendBlock()
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
