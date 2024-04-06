using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    internal class HeadingsParsingMiddlewareTests
    {
        private const float ONE_EM_IN_PIXELS = 16f;
        private const float H1_FONT_SIZE = ONE_EM_IN_PIXELS * 2f;
        private const float H2_FONT_SIZE = ONE_EM_IN_PIXELS * 1.5f;
        private const float H3_FONT_SIZE = ONE_EM_IN_PIXELS * 1.17f;
        private const float H4_FONT_SIZE = ONE_EM_IN_PIXELS * 1f;
        private const float H5_FONT_SIZE = ONE_EM_IN_PIXELS * 0.83f;
        private const float H6_FONT_SIZE = ONE_EM_IN_PIXELS * 0.67f;

        [TestCase("<h1> test content </h1>", "test content", H1_FONT_SIZE)]
        [TestCase("<h2> test content 123123 </h1>", "test content 123123", H2_FONT_SIZE)]
        [TestCase("<h3> test content 123123 </h1>", "test content 123123", H3_FONT_SIZE)]
        [TestCase("<h4> test content 123123 </h1>", "test content 123123", H4_FONT_SIZE)]
        [TestCase("<h5> test content 123123 </h1>", "test content 123123", H5_FONT_SIZE)]
        [TestCase("<h6> test content 123123 </h1>", "test content 123123", H6_FONT_SIZE)]
        public void ParseHeader_SingleHeader_ReturnSingleRowWithFontSizes
            (string html, string expectedContent, float expectedFontSize)
        {
            // Arrange
            var sut = CreateSut();
            var expectedFontStyle = CreationRowHelper.CreateTextStyle(fontSize: expectedFontSize);
            var expectedDocument = new Document().AppendRow().AppendBlock()
                .AppendContent(expectedContent, expectedFontStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<h1> test con <br> tent 123123</h1>", "test con", "tent 123123", H1_FONT_SIZE)]
        [TestCase("<h2> test con <br> tent 123123 </h1>", "test con", "tent 123123", H2_FONT_SIZE)]
        [TestCase("<h3> test con <br> tent 123123 </h1>", "test con", "tent 123123", H3_FONT_SIZE)]
        [TestCase("<h4> test con <br> tent 123123 </h1>", "test con", "tent 123123", H4_FONT_SIZE)]
        [TestCase("<h5> test con <br> tent 123123 </h1>", "test con", "tent 123123", H5_FONT_SIZE)]
        [TestCase("<h6> test con <br> tent 123123 </h1>", "test con", "tent 123123", H6_FONT_SIZE)]
        public void ParseHeader_SingleHeaderWithInnerBreakLine_ReturnTwoRowWithFontSizes
            (string html, string firstExpectedContent, string secondExpectedContent, float expectedFontSize)
        {
            // Arrange
            var sut = CreateSut();
            var expectedFontStyle = CreationRowHelper.CreateTextStyle(fontSize: expectedFontSize);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstExpectedContent, expectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(secondExpectedContent, expectedFontStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<h1> test con <p> tent 123123 </p> </h1>", "test con", "tent 123123", H1_FONT_SIZE)]
        [TestCase("<h2> test con <p> tent 123123 </p> </h1>", "test con", "tent 123123", H2_FONT_SIZE)]
        [TestCase("<h3> test con <p> tent 123123 </p> </h1>", "test con", "tent 123123", H3_FONT_SIZE)]
        [TestCase("<h4> test con <p> tent 123123 </p> </h1>", "test con", "tent 123123", H4_FONT_SIZE)]
        [TestCase("<h5> test con <p> tent 123123 </p> </h1>", "test con", "tent 123123", H5_FONT_SIZE)]
        [TestCase("<h6> test con <p> tent 123123 </p> </h1>", "test con", "tent 123123", H6_FONT_SIZE)]
        public void ParseHeader_SingleHeaderWithInnerParagraph_ReturnTwoRowWithFontSizes
            (string html, string firstExpectedContent, string secondExpectedContent, float expectedFontSize)
        {
            // Arrange
            var sut = CreateSut();
            var expectedFontStyle = CreationRowHelper.CreateTextStyle(fontSize: expectedFontSize);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstExpectedContent, expectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(secondExpectedContent, expectedFontStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<h1> test con <h2> tent </h2> 123123 </h1>", "test con", "tent", "123123", H1_FONT_SIZE, H2_FONT_SIZE)]
        [TestCase("<h2> test con <h3> tent </h3> 123123 </h2>", "test con", "tent", "123123", H2_FONT_SIZE, H3_FONT_SIZE)]
        [TestCase("<h3> test con <h4> tent </h4> 123123 </h3>", "test con", "tent", "123123", H3_FONT_SIZE, H4_FONT_SIZE)]
        [TestCase("<h4> test con <h5> tent </h5> 123123 </h4>", "test con", "tent", "123123", H4_FONT_SIZE, H5_FONT_SIZE)]
        [TestCase("<h5> test con <h6> tent </h6> 123123 </h5>", "test con", "tent", "123123", H5_FONT_SIZE, H6_FONT_SIZE)]
        [TestCase("<h6> test con <h1> tent </h1> 123123 </h6>", "test con", "tent", "123123", H6_FONT_SIZE, H1_FONT_SIZE)]
        public void ParseHeader_SingleHeaderWithInnerOtherHeader_ReturnThreeRowWithFontSizes
            (string html, string firstExpectedContent
           , string secondExpectedContent, string thirdExpectedContent
           , float firstExpectedFontSize, float secondExpectedFontSize)
        {
            // Arrange
            var sut = CreateSut();
            var firstExpectedFontStyle = CreationRowHelper.CreateTextStyle(fontSize: firstExpectedFontSize);
            var secondExpectedFontStyle = CreationRowHelper.CreateTextStyle(fontSize: secondExpectedFontSize);
            var thirdExpectedFontStyle = CreationRowHelper.CreateTextStyle();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstExpectedContent, firstExpectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(secondExpectedContent, secondExpectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(thirdExpectedContent, thirdExpectedFontStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<h1> test con <h2> tent <h3> forth </h3> <b> sizx </b> </h2> <i> 123123 </i> </h1>", "test con", "tent", "forth", "sizx ", "123123", H1_FONT_SIZE, H2_FONT_SIZE, H3_FONT_SIZE)]
        [TestCase("<h2> test con <h3> tent <h4> forth </h4> <b> sizx </b> </h3> <i> 123123 </i> </h2>", "test con", "tent", "forth", "sizx ", "123123", H2_FONT_SIZE, H3_FONT_SIZE, H4_FONT_SIZE)]
        [TestCase("<h3> test con <h4> tent <h5> forth </h5> <b> sizx </b> </h4> <i> 123123 </i> </h3>", "test con", "tent", "forth", "sizx ", "123123", H3_FONT_SIZE, H4_FONT_SIZE, H5_FONT_SIZE)]
        [TestCase("<h4> test con <h5> tent <h6> forth </h6> <b> sizx </b> </h5> <i> 123123 </i> </h4>", "test con", "tent", "forth", "sizx ", "123123", H4_FONT_SIZE, H5_FONT_SIZE, H6_FONT_SIZE)]
        [TestCase("<h5> test con <h6> tent <h1> forth </h1> <b> sizx </b> </h6> <i> 123123 </i> </h5>", "test con", "tent", "forth", "sizx ", "123123", H5_FONT_SIZE, H6_FONT_SIZE, H1_FONT_SIZE)]
        [TestCase("<h6> test con <h1> tent <h2> forth </h2> <b> sizx </b> </h1> <i> 123123 </i> </h6>", "test con", "tent", "forth", "sizx ", "123123", H6_FONT_SIZE, H1_FONT_SIZE, H2_FONT_SIZE)]
        public void ParseHeader_SingleHeaderWithInnerOtherHeader_ReturnForthRowWithFontSizes
            (string html, string firstExpectedContent
           , string secondExpectedContent, string thirdExpectedContent
           , string forthExpectedContent, string fifthExpectedContent
           , float firstExpectedFontSize, float secondExpectedFontSize, float thirdExpectedFontSize)
        {
            // Arrange
            var sut = CreateSut();
            var firstExpectedFontStyle = CreationRowHelper.CreateTextStyle(fontSize: firstExpectedFontSize);
            var secondExpectedFontStyle = CreationRowHelper.CreateTextStyle(fontSize: secondExpectedFontSize);
            var thirdExpectedFontStyle = CreationRowHelper.CreateTextStyle(fontSize: thirdExpectedFontSize);
            var forthExpectedFontStyle = CreationRowHelper.CreateTextStyle(isBold: true);
            var fifthExpectedFontStyle = CreationRowHelper.CreateTextStyle(isItalic: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstExpectedContent, firstExpectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(secondExpectedContent, secondExpectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(thirdExpectedContent, thirdExpectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(forthExpectedContent, forthExpectedFontStyle)
                                          .AppendContent(fifthExpectedContent, fifthExpectedFontStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<h1> <b> test <h2> content </h2> 3222</h1> </b>", "test"
                , H1_FONT_SIZE, "content", H2_FONT_SIZE, "3222")]
        [TestCase("<h2> <b> test <h3> content </h3> 3222</h2> </b>", "test"
                , H2_FONT_SIZE, "content", H3_FONT_SIZE, "3222")]
        [TestCase("<h3> <b> test <h4> content </h4> 3222</h3> </b>", "test"
                , H3_FONT_SIZE, "content", H4_FONT_SIZE, "3222")]
        [TestCase("<h4> <b> test <h5> content </h5> 3222</h4> </b>", "test"
                , H4_FONT_SIZE, "content", H5_FONT_SIZE, "3222")]
        [TestCase("<h5> <b> test <h6> content </h6> 3222</h5> </b>", "test"
                , H5_FONT_SIZE, "content", H6_FONT_SIZE, "3222")]
        [TestCase("<h6> <b> test <h1> content </h1> 3222</h6> </b>", "test"
                , H6_FONT_SIZE, "content", H1_FONT_SIZE, "3222")]
        public void ParseHeader_InnerHeadersWithStyles_ReturnThreRowWithFontSizesAndStyles
            (string html, string firstExpectedContent, float firstExpectedFontSize
           , string secondExpectedContent, float secondExpectedFontSize
           , string thirdExpectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var firstExpectedFontStyle = CreationRowHelper.CreateTextStyle
                    (fontSize: firstExpectedFontSize, isBold: true);
            var secondExpectedFontStyle = CreationRowHelper.CreateTextStyle
                    (fontSize: secondExpectedFontSize, isBold: true);
            var thirdExpectedFontStyle = CreationRowHelper.CreateTextStyle
                    (fontSize: firstExpectedFontSize, isBold: true);
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstExpectedContent, firstExpectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(secondExpectedContent, secondExpectedFontStyle)
                .AppendRow().AppendBlock().AppendContent(thirdExpectedContent, thirdExpectedFontStyle)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
