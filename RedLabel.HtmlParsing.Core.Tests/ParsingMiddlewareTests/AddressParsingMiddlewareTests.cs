using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class AddressParsingMiddlewareTests
    {
        [TestCase("<address> test case </address>", "test case")]
        [TestCase("<address> \r\n test  \r\n case \r\n </address>", "test case")]
        [TestCase("<address> test12312312     case </address>", "test12312312 case")]
        [TestCase("<address> <i> test12312312     case </i> </address>", "test12312312 case")]
        [TestCase("<p> <address> <i> test12312312     case </i> </address> </p>", "test12312312 case")]
        public void ParseAddress_SingleAddress_ReturnSingleRowWithAddressStyle
            (string html, string expectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedTextStyle = CreationRowHelper.CreateTextStyle(isItalic: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(expectedContent, expectedTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<address></address>")]
        [TestCase("<p> \r\n <address></address> </p>")]
        [TestCase("<p> <br> \r\n <address></address> </p>")]
        [TestCase("<p> <br> <address><b> </b> <i> </i></address> </p>")]
        [TestCase("<p> <br> \r\n\r\n <address><b> \r\n </b> <i> \r\n </i>\r\n\r\n</address> </p>")]
        public void ParseAddress_EmptyAddress_ReturnZeroRows(string html)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document();

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<address></address> qwe121  \r\n  23qw", "qwe121 23qw")]
        [TestCase("<p>  \r\n <address></address> qwe121  23qw </p> ", "qwe121 23qw")]
        [TestCase("<p> <br> <address></address> \r\n </p> qwe121  23qw", "qwe121 23qw")]
        [TestCase("<p> <br> <address><b> </b> \r\n <i> </i></address> qwe121  23qw </p> ", "qwe121 23qw")]
        [TestCase("<p>  \r\n \r\n <br> <address><b> \r\n \r\n </b> <i> \r\n </i></address> qwe121  23qw </p> ", "qwe121 23qw")]
        public void ParseAddress_EmptyAddress_ReturnSingleRowAfter(string html, string expectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(expectedContent)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("qwe121  23qw <address></address>", "qwe121 23qw")]
        [TestCase(" qwe121  23qw<p> <address></address>\r\n </p> ", "qwe121 23qw")]
        [TestCase("<p> qwe121\r\n  23qw <br> <address></address> </p>", "qwe121 23qw")]
        [TestCase("<p> qwe121 \r\n 23qw <br> \r\n<address></address>\r\n </p>", "qwe121 23qw")]
        [TestCase("qwe121  23qw <p> <br> <address><b> </b> <i> </i></address> </p> ", "qwe121 23qw")]
        [TestCase("qwe121 \r\n\r\n 23qw <p> <br> <address><b>\r\n\r\n </b> <i> </i></address>\r\n\r\n </p> ", "qwe121 23qw")]
        public void ParseAddress_EmptyAddress_ReturnSingleRowBefore(string html, string expectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(expectedContent)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<address> first row <br> second row <p> third row </p></address>"
            , "first row", "second row", "third row")]
        [TestCase("<address> \r\n first   \r\n\r\nrow \r\n<br> second row <p>\r\n\r\n third row </p></address>\r\n\r\n"
            , "first row", "second row", "third row")]
        public void ParseAddress_AddressWithThreeRows_ReturnThreeRowsWithAddressStyle
            (string html, string firstEpectedContent, string secondExpectedContent
           , string thirdExpectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedTextStyle = CreationRowHelper.CreateTextStyle(isItalic: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstEpectedContent, expectedTextStyle)
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
