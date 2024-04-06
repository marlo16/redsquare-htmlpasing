using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class HyperlinkParsingMiddlewareTests
    {
        [TestCase("<a href=\"https://www.w3schools.com\"> Visit W3Schools.com!  </a>", "Visit W3Schools.com!", "https://www.w3schools.com")]
        public void ParseHyperlink_SingleLink_SingleRowWithHiperlink
            (string html, string expectedText, string expectedHyperLink)
        {
            // Arrange
            var sut = CreateSut();
            var hyperlinkStyle = CreationRowHelper.CreateTextStyle(hyperlink: expectedHyperLink);
            var expectedDocument = new Document().AppendRow()
                .AppendBlock().AppendContent(contentText: expectedText, hyperlinkStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<a href=\"https://www.w3schools.com\"> Visit <br> W3Schools.com!  </a>"
            , "Visit", "https://www.w3schools.com"
            , "W3Schools.com!", "https://www.w3schools.com")]
        public void ParseHyperlink_SingleLinkWithLineBreak_TwoRowsWithHiperlinks
            (string html, string firstExpectedText, string firstExpectedHyperLink
                        , string secondExpectedText, string secondExpectedHyperLink)
        {
            // Arrange
            var sut = CreateSut();
            var firstHyperlinkStyle = CreationRowHelper.CreateTextStyle(hyperlink: firstExpectedHyperLink);
            var secondHyperlinkStyle = CreationRowHelper.CreateTextStyle(hyperlink: secondExpectedHyperLink);
            var expectedDocument = new Document()
                .AppendRow()
                .AppendBlock().AppendContent(contentText: firstExpectedText, firstHyperlinkStyle)
                .AppendRow()
                .AppendBlock().AppendContent(contentText: secondExpectedText, secondHyperlinkStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }


        [TestCase("<a href=\"https://www.w3schools.com\"> Visit <a href=\"http://localhost\"> W3Scho <a href=\"http://log\"> 777777 </a> ols.com! </a>  12312312 </a>"
            , "Visit ", "https://www.w3schools.com", "12312312"
            , "W3Scho ", "http://localhost", "ols.com! "
            , "777777 ", "http://log")]
        public void ParseHyperlink_TripleNestedLinks_SingleRowWithTripleHiperlinks
            (string html, string firstExpectedText, string firstExpectedHyperLink, string firstExpectedLinkTail
            , string secondExpectedText, string secondExpectedHyperLink, string secondExpectedLinkTail
            , string thirdExpectedText, string thirdExpectedHyperLink)
        {
            // Arrange
            var sut = CreateSut();
            var tailStyle = CreationRowHelper.CreateTextStyle();
            var firstHyperlinkStyle = CreationRowHelper.CreateTextStyle(hyperlink: firstExpectedHyperLink);
            var secondHyperlinkStyle = CreationRowHelper.CreateTextStyle(hyperlink: secondExpectedHyperLink);
            var thirdHyperlinkStyle = CreationRowHelper.CreateTextStyle(hyperlink: thirdExpectedHyperLink);
            var expectedDocument = new Document().AppendRow().AppendBlock()
                .AppendContent(firstExpectedText, firstHyperlinkStyle)
                .AppendContent(secondExpectedText, secondHyperlinkStyle)
                .AppendContent(thirdExpectedText, thirdHyperlinkStyle)
                .AppendContent(secondExpectedLinkTail, tailStyle)
                .AppendContent(firstExpectedLinkTail, tailStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }


        private static HtmlParser CreateSut() => new();
    }
}
