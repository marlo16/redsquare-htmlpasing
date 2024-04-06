using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class BlockQuotationParsingMiddlewareTests
    {

        [TestCase("<blockquote cite=\"localgost\"> test paragraph </blockquote>", "test paragraph", "localgost")]
        [TestCase("<p><blockquote cite=\"localgost\"> test paragraph </blockquote></p>", "test paragraph", "localgost")]
        [TestCase("<p></p><blockquote cite=\"localgost\"> test paragraph </blockquote>", "test paragraph", "localgost")]
        [TestCase("<blockquote cite=\"localgost\"> <p>test paragraph </p></blockquote>", "test paragraph", "localgost")]
        public void ParseBlockQuotation_SingleBlockQuotation_ReturnRowWithIndentation
            (string html, string expectedContent, string expectedTitle)
        {
            // Arrange
            var sut = CreateSut();
            var expectedRowStyle = CreationRowHelper.CreateRowStyle(indentationCoefficient: 1);
            var expectedTextStyle = CreationRowHelper.CreateTextStyle(title: expectedTitle);
            var expectedDocument = new Document()
                .AppendRow(expectedRowStyle).AppendBlock().AppendContent(expectedContent, expectedTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<br><blockquote cite=\"localgost\"> test paragraph </blockquote>", "test paragraph", "localgost")]
        public void ParseBlockQuotation_SingleBlockQuotationWithOutsideBreakRow_ReturnRowWithIndentationAndemptyRow
            (string html, string expectedContent, string expectedTitle)
        {
            // Arrange
            var sut = CreateSut();
            var expectedRowStyle = CreationRowHelper.CreateRowStyle(indentationCoefficient: 1);
            var expectedTextStyle = CreationRowHelper.CreateTextStyle(title: expectedTitle);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(string.Empty)
                .AppendRow(expectedRowStyle).AppendBlock().AppendContent(expectedContent, expectedTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<blockquote cite=\"localgost\"> test <br> paragraph </blockquote>", "test", "paragraph", "localgost")]
        public void ParseBlockQuotation_SingleBlockQuotationWithInnerBreakRow_ReturnTwoRowsWithIndentation
            (string html, string firstExpectedContent, string secondExpectedContent, string expectedTitle)
        {
            // Arrange
            var sut = CreateSut();
            var expectedRowStyle = CreationRowHelper.CreateRowStyle(indentationCoefficient: 1);
            var expectedTextStyle = CreationRowHelper.CreateTextStyle(title: expectedTitle);
            var expectedDocument = new Document()
                .AppendRow(expectedRowStyle).AppendBlock().AppendContent(firstExpectedContent, expectedTextStyle)
                .AppendRow(expectedRowStyle).AppendBlock().AppendContent(secondExpectedContent, expectedTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<blockquote cite=\"localgost\"> test paragraph <blockquote cite=\"loca1231slgost\"> seconsd 3q231 </blockquote></blockquote>"
            , "test paragraph", "localgost", 1
            , "seconsd 3q231", "loca1231slgost", 2)]
        [TestCase("<blockquote cite=\"localgost\"> test paragraph </blockquote> <blockquote cite=\"loca1231slgost\"> seconsd 3q231 </blockquote>"
            , "test paragraph", "localgost", 1
            , "seconsd 3q231", "loca1231slgost", 1)]
        public void ParseBlockQuotation_SingleBlockQuotationWithInnerBlock_ReturnTwoRowsWithIndentations
            (string html, string firstExpectedContent, string firstExpectedTitle, int firstIndentatioCoefficient
                        , string secondExpectedContent, string secondExpectedTitle, int secondIndentatioCoefficient)
        {
            // Arrange
            var sut = CreateSut();
            var firstExpectedRowStyle = CreationRowHelper.CreateRowStyle(indentationCoefficient: firstIndentatioCoefficient);
            var secondExpectedRowStyle = CreationRowHelper.CreateRowStyle(indentationCoefficient: secondIndentatioCoefficient);
            var firstExpectedTextStyle = CreationRowHelper.CreateTextStyle(title: firstExpectedTitle);
            var secondExpectedTextStyle = CreationRowHelper.CreateTextStyle(title: secondExpectedTitle);
            var expectedDocument = new Document()
                .AppendRow(firstExpectedRowStyle).AppendBlock().AppendContent(firstExpectedContent, firstExpectedTextStyle)
                .AppendRow(secondExpectedRowStyle).AppendBlock().AppendContent(secondExpectedContent, secondExpectedTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
