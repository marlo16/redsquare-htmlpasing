using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class PreformattedParsingMiddlewareTests
    {
        [TestCase("<pre>  saasdasdw34   54563456 gfgyhy     </pre>", "  saasdasdw34   54563456 gfgyhy     ")]
        [TestCase("<p>  \r\n  <pre>  saasdasdw34   54563456 gfgyhy    </pre>  \r\n </p> ", "  saasdasdw34   54563456 gfgyhy    ")]
        public void ParsePreformattedText_TextWithWhiteSpaces_ReturnRowWithWhiteSpaces
            (string html, string expectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedTextStyle = CreationRowHelper.CreateTextStyle(isPreformattedText: true);
            var expectedDocument = new Document().AppendRow()
                .AppendBlock().AppendContent(expectedContent, expectedTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<pre>  saasdasdw34   <p>   </p> 54563456 gfgyhy     </pre>", "  saasdasdw34   ", "   ", " 54563456 gfgyhy     ")]
        [TestCase("<pre>\r\n  Text in a pre element\r\n  is displayed in a fixed-width  \r\n      in a pre element   </pre>"
            , "  Text in a pre element", "  is displayed in a fixed-width  ", "      in a pre element   ")]
        [TestCase("<pre> \r\n  Text in a pre element\r\n  is displayed in a fixed-width   in a pre element   </pre>"
            , " ", "  Text in a pre element", "  is displayed in a fixed-width   in a pre element   ")]
        public void ParsePreformattedText_TextWithWhiteSpacesAndInnerParagraph_ReturnThreeRowsWithWhiteSpaces
            (string html, string firstExpectedContent, string secondExpectedContent, string thirdExpectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedTextStyle = CreationRowHelper.CreateTextStyle(isPreformattedText: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstExpectedContent, expectedTextStyle)
                .AppendRow().AppendBlock().AppendContent(secondExpectedContent, expectedTextStyle)
                .AppendRow().AppendBlock().AppendContent(thirdExpectedContent, expectedTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
