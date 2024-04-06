using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class BiDirectionParsingMiddlewareTests
    {
        [TestCase("<bdo dir=\"rtl\"> 1234567 890 </bdo>", "098 7654321", TextDirectionType.RightToLeft)]
        [TestCase("<p> <bdo dir=\"rtl\"> 1234567 890 </bdo> </p>", "098 7654321", TextDirectionType.RightToLeft)]
        [TestCase("<p> <bdo dir=\"rtl\"> 1234567 890 asd</bdo> </p>", "dsa 098 7654321", TextDirectionType.RightToLeft)]
        [TestCase("<bdo dir=\"ltr\"> 1234567 890 </bdo>", "1234567 890", TextDirectionType.LeftToRight)]
        [TestCase("<p> <bdo dir=\"ltr\"> 1234567 890 </bdo> </p>", "1234567 890", TextDirectionType.LeftToRight)]
        [TestCase("<p> <bdo dir=\"ltr\"> 1234567 890 asd</bdo> </p>", "1234567 890 asd", TextDirectionType.LeftToRight)]
        [TestCase("<bdo> 1234567 890 </bdo>", "1234567 890", TextDirectionType.LeftToRight)]
        [TestCase("<p> <bdo> 1234567 890 </bdo> </p>", "1234567 890", TextDirectionType.LeftToRight)]
        [TestCase("<p> <bdo> 1234567 890 asd</bdo> </p>", "1234567 890 asd", TextDirectionType.LeftToRight)]
        [TestCase("<bdo> 1234567 890 </bdo>", "1234567 890", TextDirectionType.LeftToRight)]
        [TestCase("<p> 1234567 890 </p>", "1234567 890", TextDirectionType.LeftToRight)]
        [TestCase("<p> 1234567 890 asd </p>", "1234567 890 asd", TextDirectionType.LeftToRight)]
        public void ParseBiDirection_SingleTextWithDirection_ReturnSingleRowWithDirection
            (string html, string expectedContent, TextDirectionType expectedDirectionType)
        {
            // Arrange
            var sut = CreateSut();
            var expectedTextStyle = CreationRowHelper.CreateTextStyle
                (textDirectionType: expectedDirectionType);
            var expectedDocument = new Document().AppendRow().AppendBlock()
                .AppendContent(expectedContent, expectedTextStyle).ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<bdo dir=\"rtl\"> 1234567 <br> 890 </bdo>", "7654321", "098"
                 , TextDirectionType.RightToLeft, TextDirectionType.RightToLeft)]
        [TestCase("<bdo dir=\"ltr\"> 1234567 <br> 890 </bdo>", "1234567", "890"
                 , TextDirectionType.LeftToRight, TextDirectionType.LeftToRight)]
        [TestCase("<bdo dir=\"ltr\"> 1234567 </bdo> <br> <bdo dir=\"rtl\"> 890 </bdo>", "1234567", "098"
                 , TextDirectionType.LeftToRight, TextDirectionType.RightToLeft)]
        [TestCase("<bdo dir=\"ltr\"> 1234567  <br> <bdo dir=\"rtl\"> 890 </bdo> </bdo>", "1234567", "098"
                 , TextDirectionType.LeftToRight, TextDirectionType.RightToLeft)]
        public void ParseBiDirection_TwoTextsWithDirections_ReturnTwoRowsWithDirection
            (string html, string firstExpectedContent, string secondExpectedContent
           , TextDirectionType firstExpectedDirectionType, TextDirectionType secondExpectedDirectionType)
        {
            // Arrange
            var sut = CreateSut();
            var firstExpectedTextStyle = CreationRowHelper.CreateTextStyle
                (textDirectionType: firstExpectedDirectionType);
            var secondExpectedTextStyle = CreationRowHelper.CreateTextStyle
                (textDirectionType: secondExpectedDirectionType);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstExpectedContent, firstExpectedTextStyle)
                .AppendRow().AppendBlock().AppendContent(secondExpectedContent, secondExpectedTextStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
