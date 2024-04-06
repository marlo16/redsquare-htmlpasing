using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class TitleParsingMiddlewareTests
    {
        [TestCase("\r\n <title>123 \r\n 1as23 \r\n</title> \r\n", "123 1as23")]
        [TestCase("\r\n <title>123 \r\n <br>1as2 <p>3</p> \r\n</title> \r\n", "123 <br>1as2 <p>3</p>")]
        public void ParseTitle_DocumentTitleEmpty_SetDocumentTitle(string html, string expectedTitle)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document();
            expectedDocument.Title = expectedTitle;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
