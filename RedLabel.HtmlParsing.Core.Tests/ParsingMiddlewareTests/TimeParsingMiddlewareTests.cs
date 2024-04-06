using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class TimeParsingMiddlewareTests
    {
        [TestCase("<time datetime=\"2008-02-14 20:00\">Valentines day</time>", "Valentines day", "2008-02-14 20:00")]
        public void ParseTime_MainScenario_ReturnContentWithTimeTitle
            (string html, string expectedContent, string expectedTitle)
        {
            // Arrange
            var sut = CreateSut();
            var expectentContentStyle = CreationRowHelper.CreateTextStyle(title: expectedTitle);
            var expectedDocument = new Document().AppendRow().AppendBlock()
                .AppendContent(expectedContent, expectentContentStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
