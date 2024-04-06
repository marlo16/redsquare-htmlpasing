using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class AbbreviationParsingMiddlewareTests
    {
        [TestCase("<abbr title=\"World Health Organization\">WHO</abbr>", "WHO", "World Health Organization")]
        [TestCase("<abbr \r\n title=\"World Health \r\n Organization\">WHO \r\n\r\n</abbr>", "WHO", "World Health \r\n Organization")]
        [TestCase("<p> <abbr title=\"  World Health Organization   \">WHO</abbr> </p>", "WHO", "World Health Organization")]
        [TestCase("<p> \r\n\r\n <abbr title=\" \r\n World Health Organization  \r\n \">WHO</abbr> </p>", "WHO", "World Health Organization")]
        public void ParseAbbreviation_SingleTextWithTitle_ReturnSingleRowWithContentTitle
            (string html, string expectedAbbreviation, string exptectedTitle)
        {
            // Arrange
            var sut = CreateSut();
            var textStyle = CreationRowHelper.CreateTextStyle(title: exptectedTitle);
            var expectedDocument = new Document().AppendRow().AppendBlock()
                                                 .AppendContent(expectedAbbreviation, textStyle)
                                                 .ParentRow.ParentDocument;
            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<abbr title=\"World Health Organization\"> WHO <abbr title=\"seconsart\">12123qweqwe  </abbr>  </abbr>"
                , "WHO ", "World Health Organization", "12123qweqwe", "seconsart")]
        public void ParseAbbreviation_SingleTextWithTwoTitle_ReturnSingleRowWithTwoContentTitles
            (string html, string firstExpectedAbbreviation, string firstExptectedTitle
                        , string secondExpectedAbbreviation, string secondExptectedTitle)
        {
            // Arrange
            var sut = CreateSut();
            var firstTextStyle = CreationRowHelper.CreateTextStyle(title: firstExptectedTitle);
            var secondTextStyle = CreationRowHelper.CreateTextStyle(title: secondExptectedTitle);
            var expectedDocument = new Document().AppendRow().AppendBlock()
                                                 .AppendContent(firstExpectedAbbreviation, firstTextStyle)
                                                 .AppendContent(secondExpectedAbbreviation, secondTextStyle)
                                                 .ParentRow.ParentDocument;
            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<abbr title=\"World Health Organization\"> WHO <abbr title=\"seconsart\">12123qweqwe  </abbr>  therdt121 </abbr>"
                , "WHO ", "World Health Organization"
                , "12123qweqwe ", "seconsart"
                , "therdt121", "World Health Organization")]
        public void ParseAbbreviation_OtherTitleInTheMiddle_ReturnSingleRowWithThreeContentTitles
            (string html, string firstExpectedAbbreviation, string firstExptectedTitle
                        , string secondExpectedAbbreviation, string secondExptectedTitle
                        , string thirdExpectedAbbreviation, string thirdExptectedTitle)
        {
            // Arrange
            var sut = CreateSut();
            var firstTextStyle = CreationRowHelper.CreateTextStyle(title: firstExptectedTitle);
            var secondTextStyle = CreationRowHelper.CreateTextStyle(title: secondExptectedTitle);
            var thirdTextStyle = CreationRowHelper.CreateTextStyle(title: thirdExptectedTitle);
            var expectedDocument = new Document().AppendRow().AppendBlock()
                                                 .AppendContent(firstExpectedAbbreviation, firstTextStyle)
                                                 .AppendContent(secondExpectedAbbreviation, secondTextStyle)
                                                 .AppendContent(thirdExpectedAbbreviation, thirdTextStyle)
                                                 .ParentRow.ParentDocument;
            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
