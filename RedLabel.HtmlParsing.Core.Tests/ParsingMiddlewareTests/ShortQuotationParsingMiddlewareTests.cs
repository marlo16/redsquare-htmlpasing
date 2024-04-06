using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class ShortQuotationParsingMiddlewareTests
    {
        private const string OPEN_QUOTATION_SYMBOL = "\"";
        private const string CLOSE_QUOTATION_SYMBOL = "\"";

        [TestCase("<q>    \r\n\r\ndsfgsdr 123   </q>", " dsfgsdr 123 ")]
        [TestCase("     <q>    dsfgsdr \r\n\r\n123   </q>    ", " dsfgsdr 123 ")]
        [TestCase("  <p>   <q> \r\n   dsfgsdr 123   </q>  </p>  ", " dsfgsdr 123 ")]
        [TestCase("  <p>  </p> \r\n <p>  <p> <q>  \r\n  dsfgsdr 123 </p>  </q>  </p>  <p>  </p>", " dsfgsdr 123 ")]
        public void ParseQuotation_SingleText_ReturnSingleRowWithThreeContents
            (string html, string expectedText)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document().AppendRow().AppendBlock()
                .AppendContent(OPEN_QUOTATION_SYMBOL)
                .AppendContent(expectedText)
                .AppendContent(CLOSE_QUOTATION_SYMBOL).ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<q>    dsfgs12132 <br>  dr 123   </q>", " dsfgs12132", "dr 123 ")]
        [TestCase("<q>  <i></i>  dsfgs12132 <br>  dr 123  <i></i> </q>", " dsfgs12132", "dr 123 ")]
        [TestCase("  <p> <q>  <i></i>  dsfgs12132 <br>  dr 123  <i></i> </q>  </p>  ", " dsfgs12132", "dr 123 ")]
        [TestCase("<q>  \r\n\r\n  dsfgs1213\r\n2 <br>  dr \r\n123  \r\n </q>", " dsfgs1213 2", "dr 123 ")]
        [TestCase("<q>  <i></i>  dsfgs\r\n\r\n12132 <br>  dr 123  <i></i> </q>\r\n", " dsfgs 12132", "dr 123 ")]
        [TestCase("  <p> <q> \r\n <i></i>  dsfgs\r\n12132 <br> \r\n\r\n dr 123  <i></i>\r\n</q>  </p>  ", " dsfgs 12132", "dr 123 ")]
        public void ParseQuotation_PartitionText_ReturnTwoQuotedRows
            (string html, string firstExpectedText, string secondExpectedText)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock()
                            .AppendContent(OPEN_QUOTATION_SYMBOL)
                            .AppendContent(firstExpectedText)
                .AppendRow().AppendBlock()
                            .AppendContent(secondExpectedText)
                            .AppendContent(CLOSE_QUOTATION_SYMBOL)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<i>  <q>    dsfgs12132 <br>  dr 123   </q>  </i>", " dsfgs12132", "dr 123 ")]
        [TestCase("<i>  <q>    dsfgs12132 <br>  dr 123   </q>  </i>", " dsfgs12132", "dr 123 ")]
        [TestCase("<p> <i>  <q>  \r\n  dsfgs12132 <br>  dr 123 \r\n  </q>  </i>\r\n </p>", " dsfgs12132", "dr 123 ")]
        [TestCase("<p>\r\n\r\n <i>  <q> \r\n   dsfgs12132 <br>  dr 123 \r\n  </q>  </i> </p>", " dsfgs12132", "dr 123 ")]
        public void ParseQuotation_StyledPartitionText_ReturnTwoQuotedRowsTiwhStyles
            (string html, string firstExpectedText, string secondExpectedText)
        {
            // Arrange
            var sut = CreateSut();
            var textStyle = CreationRowHelper.CreateTextStyle(isItalic: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock()
                            .AppendContent(OPEN_QUOTATION_SYMBOL, textStyle)
                            .AppendContent(firstExpectedText, textStyle)
                .AppendRow().AppendBlock()
                            .AppendContent(secondExpectedText, textStyle)
                            .AppendContent(CLOSE_QUOTATION_SYMBOL, textStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
