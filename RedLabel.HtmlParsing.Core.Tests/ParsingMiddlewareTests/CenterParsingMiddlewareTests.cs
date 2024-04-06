using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class CenterParsingMiddlewareTests
    {
        [TestCase("<center> rewetasds 123 </center>", "rewetasds 123")]
        [TestCase("<center> <p> rewetasds 123 </p> </center>", "rewetasds 123")]
        [TestCase("<p> <center> rewetasds 123 </center> </p>", "rewetasds 123")]
        public void ParseCenter_SingleCenter_ReturnRowWithAlignment
            (string html, string expectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var rowStyle = CreationRowHelper.CreateRowStyle
                (horizontalAlignment: HorizontalAlignment.Center);
            var expectedDocument = new Document()
                .AppendRow(rowStyle).AppendBlock().AppendContent(expectedContent)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<center> rewetasds 123 </center> <center> tes14 </center>", "rewetasds 123", "tes14")]
        [TestCase("<p> <center> rewetasds 123 </center> <center> tes14 </center> </p>", "rewetasds 123", "tes14")]
        [TestCase("<center> rewetasds 123 <center> tes14 </center> </center> ", "rewetasds 123", "tes14")]
        [TestCase("<p> <center> rewetasds 123 </center> <p> <center> tes14 </center>", "rewetasds 123", "tes14")]
        public void ParseCenter_TwoCenters_ReturnTwoRowsWithAlignment
            (string html, string firstExpectedContent, string secondExpectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var rowStyle = CreationRowHelper.CreateRowStyle
                (horizontalAlignment: HorizontalAlignment.Center);
            var expectedDocument = new Document()
                .AppendRow(rowStyle).AppendBlock().AppendContent(firstExpectedContent)
                .AppendRow(rowStyle).AppendBlock().AppendContent(secondExpectedContent)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<br> <center> rewetasds 123 </center>", "rewetasds 123")]
        [TestCase("<br> <center> <p> rewetasds 123 </p> </center>", "rewetasds 123")]
        [TestCase("<br> <p> <center> rewetasds 123 </center> </p>", "rewetasds 123")]
        public void ParseCenter_SingleCenterWithLineBreakBefore_ReturnEmptyRowAndRowWithAlignment
            (string html, string expectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var rowStyle = CreationRowHelper.CreateRowStyle
                (horizontalAlignment: HorizontalAlignment.Center);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(string.Empty)
                .AppendRow(rowStyle).AppendBlock().AppendContent(expectedContent)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
