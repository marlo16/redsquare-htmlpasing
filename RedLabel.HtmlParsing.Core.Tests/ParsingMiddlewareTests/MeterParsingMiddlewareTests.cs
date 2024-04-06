using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class MeterParsingMiddlewareTests
    {
        [TestCase("<meter value=\"2\"></meter>", "100")]
        [TestCase("<meter value=\"1\" min=\"0\" max=\"2\">/meter>", "50")]
        [TestCase("<meter value=\"-1\" min=\"0\" max=\"2\"></meter>", "0")]
        [TestCase("<meter \r\n value=\"10000\" min=\"0\" max=\"2\"></meter>", "100")]
        [TestCase("<meter value=\"10000\" min=\"10000\" \r\n\r\n max=\"2\"></meter>", "0")]
        [TestCase("<meter>\r\n</meter>", "0")]
        [TestCase("<meter value=\"1\" min=\"1\" max=\"2\">/meter>", "0")]
        [TestCase("<meter \r\n value=\"0\" min=\"1\" max=\"2\">/meter>", "0")]
        public void ParseMeter_SingleMeter_ReturnOneRowWithBarElementContent(string html, string expectedPercentage)
        {
            // Arrange
            var sut = CreateSut();
            var expectedContentStyle = CreationRowHelper.CreateTextStyle(backgroundColor: "#00ff00");
            var expectedDocument = new Document().AppendRow().AppendBlock().AppendContent
                (expectedPercentage, expectedContentStyle, isBarElementContent: true)
                .ParentRow.ParentDocument;
            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
