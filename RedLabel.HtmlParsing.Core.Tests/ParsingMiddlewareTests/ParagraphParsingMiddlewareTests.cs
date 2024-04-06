using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class ParagraphParsingMiddlewareTests
    {
        [TestCase("<p></p>")]
        [TestCase("   <p>    </p>")]
        [TestCase(" \r\n  <p>  \r\n   </p>")]      
        [TestCase("   <p>  <p>  <p> </p> </p>  </p>")]
        [TestCase("  <p> </p> <p>  <p>  <p> </p> </p>  </p> <p> </p>")]
        [TestCase(" <b> <p> </p> <p>  <p>  <p> </p> </p>  </p> <p> </p>  </b>")]
        [TestCase(" <b> <p> </p> <p> <i>  <p>  <p> </p> </p>  </i> </p> <p> </p>  </b>")]
        [TestCase(" \r\n  <p> <p> \r\n  <p> </p> </p>  \r\n  </p>")]
        public void ParseParagraph_EmptyParagraph_ReturnEmptyDocument(string html)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document();

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<p>123 testCase</p>", "123 testCase")]
        [TestCase("<p>  123 testCase</p>", "123 testCase")]
        [TestCase("<p>123 testCase   </p>", "123 testCase")]
        [TestCase("<p>    123 testCase   </p>", "123 testCase")]
        [TestCase("<p>  \r\n  123 testCase \r\n  </p> \r\n", "123 testCase")]
        [TestCase("   <p>123 testCase   </p>", "123 testCase")]
        [TestCase("<p>123 testCase   </p>    ", "123 testCase")]
        [TestCase("   <p>123 testCase   </p>   ", "123 testCase")]
        [TestCase("  <p>  </p>123 testCase", "123 testCase")]
        [TestCase(" <p>  </p> <p> <p>  </p> </p>123 testCase    ", "123 testCase")]
        [TestCase(" <p>  </p> <p> <p>  </p> <p>  </p> </p>123 testCase    <p>  </p> <p> <p> <p>  </p> </p> </p>", "123 testCase")]
        [TestCase(" \r\n <p>  \r\n\r\n </p> <p> <p> \r\n\r\n\r\n </p> <p>  </p> </p>123 testCase   \r\n\r\n\r\n <p>  </p> <p> <p> <p>  </p> </p> </p>", "123 testCase")]
        [TestCase("   123 testCase    <p>  </p> <p> <p>  </p> </p> ", "123 testCase")]
        [TestCase("<p> <p> <p>  <p> 123 testCase   </p>  </p>  </p>  </p>  ", "123 testCase")]
        [TestCase("<p> <p> <p> </p> <p>  <p> 123 testCase   </p>  </p> <p> </p> </p>  </p>  ", "123 testCase")]
        [TestCase("   123 testCase   ", "123 testCase")]
        [TestCase(" \r\n <p>123 testCase  \r\n </p> \r\n ", "123 testCase")]
        public void ParseParagraph_SingleParagraph_ReturnSingleRow(string html, string paragrapgContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(paragrapgContent)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<p> 123 testCase <p> innerTestCase </p> </p>", "123 testCase", "innerTestCase")]
        [TestCase("<p> 123 testCase </p>  <p> innerTestCase </p>", "123 testCase", "innerTestCase")]
        [TestCase("<p><p> 123 testCase </p>  innerTestCase </p>", "123 testCase", "innerTestCase")]
        [TestCase("<p>   \r\n   <p> 123 testCase </p>  innerTestCase </p> \r\n  ", "123 testCase", "innerTestCase")]
        [TestCase("    <p> 123 testCase </p>    <p>  \r\n     innerTestCase </p>   ", "123 testCase", "innerTestCase")]
        [TestCase("  123 testCase <p> innerTestCase </p>", "123 testCase", "innerTestCase")]
        [TestCase(" <p> 123 testCase </p> innerTestCase ", "123 testCase", "innerTestCase")]
        [TestCase("  123 testCase <p>  </p> innerTestCase ", "123 testCase", "innerTestCase")]
        [TestCase("  <p>  </p> <p> <p> \r\n </p> </p> 123 testCase <p>  </p> innerTestCase <p>  <p>  </p></p> <p>  </p>", "123 testCase", "innerTestCase")]
        [TestCase(" 123 testCase <p>  </p> innerTestCase <p>  <p>  </p></p> <p>  </p>", "123 testCase", "innerTestCase")]
        [TestCase("  <p>  </p> <p> <p>  </p> </p> 123 testCase <p>  </p> innerTestCase", "123 testCase", "innerTestCase")]
        [TestCase("  <p> <p> </p> <p> <p> 123 testCase </p> </p>  </p> <p> <p>innerTestCase </p></p>", "123 testCase", "innerTestCase")]
        [TestCase(" <p> <p> <p> <p> </p> <p> <p> 123 testCase </p> </p>  </p> <p> <p>innerTestCase </p></p> </p> </p>", "123 testCase", "innerTestCase")]
        [TestCase(" \r\n <p> 123 testCase </p>   \r\n <p>   \r\n\r\ninnerTestCase </p> \r\n ", "123 testCase", "innerTestCase")]
        public void ParseParagraph_TwoParagrapghs_ReturnTwoRows(string html, string firstRowContent
                                                              , string secondRowContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstRowContent)
                .AppendRow().AppendBlock().AppendContent(secondRowContent)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<p> 123 testCase <p> innerTestCase <p> third case </p></p> </p>", "123 testCase", "innerTestCase", "third case")]
        [TestCase("<p> 123 testCase <p> <p> third case </p> innerTestCase </p> </p>", "123 testCase", "third case", "innerTestCase")]
        [TestCase("<p><p> 123 testCase </p>  <p> third case </p> innerTestCase </p>", "123 testCase", "third case", "innerTestCase")]
        [TestCase("<p>      <p> 123 testCase </p>  innerTestCase </p>   <p> third case </p>", "123 testCase", "innerTestCase", "third case")]
        [TestCase(" 123 testCase <p> innerTestCase </p> third case ", "123 testCase", "innerTestCase", "third case")]
        [TestCase(" 123 testCase <p>  </p> innerTestCase <p>  </p> third case ", "123 testCase", "innerTestCase", "third case")]
        [TestCase(" 123 testCase <p>  </p> innerTestCase <p>  third case </p> ", "123 testCase", "innerTestCase", "third case")]
        [TestCase("   <p>  </p>  <p>123 testCase  </p>  innerTestCase <p>  third case </p>   <p>  </p> ", "123 testCase", "innerTestCase", "third case")]
        [TestCase("  <p>  </p>  <p>  <p>  <p>  </p> </p> </p>  <p>123 testCase  </p>  innerTestCase <p>  third case </p>   <p>  <p>  </p> </p>  <p>  </p> ", "123 testCase", "innerTestCase", "third case")]
        [TestCase("123 testCase  <p>  innerTestCase </p>  third case </p>   <p>  <p>  </p> </p>  <p>  </p> ", "123 testCase", "innerTestCase", "third case")]
        [TestCase("  <p>  </p>  <p>  <p>  <p>  </p> </p> </p>  <p>123 testCase  </p>  innerTestCase <p>third case", "123 testCase", "innerTestCase", "third case")]
        [TestCase("    <p> 123 testCase </p>    <p>       innerTestCase </p>   <p>   third case   </p>  ", "123 testCase", "innerTestCase", "third case")]
        [TestCase(" <p>  <p> <p> </p> <p>  <p> </p>123 testCase </p>  </p>   <p>       innerTestCase </p>   <p>   third case   </p> </p> ", "123 testCase", "innerTestCase", "third case")]
        [TestCase(" <p>  <p> <p> </p> <p>  <p> </p>123 testCase </p>  </p>   <p>   <p> </p>   <p> </p>  innerTestCase </p> <p> </p>   <p> <p> </p>  <p>  third case </p>  </p> </p> ", "123 testCase", "innerTestCase", "third case")]
        [TestCase(" \r\n <p> 123 testCase </p>  \r\n  <p>  \r\n\r\n innerTestCase </p> \r\n\r\n <p>  third case \r\n\r\n  </p> ", "123 testCase", "innerTestCase", "third case")]
        public void ParseParagraph_ThreeParagrapghs_ReturnThreeRows(string html, string firstRowContent
                                                                  , string secondRowContent, string thirdRowContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstRowContent)
                .AppendRow().AppendBlock().AppendContent(secondRowContent)
                .AppendRow().AppendBlock().AppendContent(thirdRowContent)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<p> <b>123 testCase</b> </p>", "123 testCase")]
        [TestCase("<b> <p>  123 testCase</p> </b>", "123 testCase")]
        [TestCase("<p>  <b>  123 testCase   </p>  </b>", "123 testCase")]
        [TestCase("<b>  <p>    123 testCase  </b> </p>", "123 testCase")]
        [TestCase("<b>   <p>123 testCase   </p>   </b>", "123 testCase")]
        [TestCase("<p> <p> <p><b>   <p>123 testCase   </p>  </p>  </b> </p> </p>", "123 testCase")]
        [TestCase("<p></p> <p><p></p></p><b> <p></p> <p><p></p></p>   <p>123 testCase   </p>   </b>", "123 testCase")]
        [TestCase(" <b> <p>123 testCase   </p> <p></p> <p><p></p></p><b> <p></p> <p><p></p></p>    </b>", "123 testCase")]
        [TestCase("  <p></p> <p><p></p></p><b> <p></p> <p><p></p></p> <b> <p>123 testCase   </p> <p></p> <p><p></p></p><b> <p></p> <p><p></p></p>    </b>", "123 testCase")]
        [TestCase(" \r\n <b> \r\n <p>123 testCase  \r\n\r\n </p> \r\n </b> \r\n\r\n", "123 testCase")]
        public void ParseParagraph_SingleParagraphWithStyle_ReturnSingleRowWithStyle(string html, string paragrapgContent)
        {
            // Arrange
            var sut = CreateSut();
            var textStyle = CreationRowHelper.CreateTextStyle(isBold: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(paragrapgContent, textStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<p> <b>123 testCase <i>42 tst tt</i> </b>  </p>", "123 testCase ", "42 tst tt")]
        [TestCase("<b> <p>  123 testCase <i>  42 tst tt  </i> </p> </b>", "123 testCase ", "42 tst tt")]
        [TestCase("<p>  <b>  123 testCase   <i>  42 tst tt  </i> </p>   </b>", "123 testCase ", "42 tst tt")]
        [TestCase(" <p> <p> <p>  <b>  123 testCase   <i>  42 tst tt  </i> </p>   </b> </p> </p>", "123 testCase ", "42 tst tt")]
        [TestCase(" <p> <p> <p></p> <p>  <b>  123 testCase   <i>  42 tst tt </i> </p>   </b> </p> </p>", "123 testCase ", "42 tst tt")]
        [TestCase(" <b> <p> <p> <p></p> <p>   123 testCase   <i>  42 tst tt </i> </p>   </b> </p> </p>", "123 testCase ", "42 tst tt")]
        [TestCase("\r\n\r\n <b> \r\n\r\n <p>123 testCase   \r\n\r\n<i> 42 tst tt  </i>   </p>  </b> \r\n", "123 testCase ", "42 tst tt")]
        public void ParseParagraph_SingleParagraphTwoContentsWithStyle_ReturnSingleRowWithTwoContentsAndStyles
            (string html, string firstContent, string secondContent)
        {
            // Arrange
            var sut = CreateSut();
            var firstTextStyle = CreationRowHelper.CreateTextStyle(isBold: true);
            var secondTextStyle = CreationRowHelper.CreateTextStyle(isBold: true, isItalic: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstContent, firstTextStyle)
                                          .AppendContent(secondContent, secondTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
