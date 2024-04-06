using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class BreakParsingMiddlewareTests
    {
        [TestCase("<br>")]
        [TestCase("<br> <br>")]
        [TestCase(" <br>  <br> <br> <br>")]
        [TestCase("<p> <br>  <br> <br> <br> </p>")]
        [TestCase("<p> <br>  <br> <p> <p> </p> </p> <br> <br> </p>")]
        [TestCase("   <br>   <i>    </i>")]
        [TestCase("<p> <br> <i> </i>  <i><br> <br> <br></i> </p>")]
        [TestCase(@"<p> <br>  
                        <br> <p> <p> </p>
                    </p> <br> <br> </p>")]
        public void ParseBreak_EmptyHtml_ReturnEmptyDocument(string html)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document();

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase("<br>  test case", "test case")]
        [TestCase("<p> <br>  test case </p>", "test case")]
        [TestCase("<p>  <i>   </i> <br>  test case </p>", "test case")]
        [TestCase("<p> </p> <p>  <i>   </i> <br>  test case </p>", "test case")]
        [TestCase("<p> </p> <p>  <i>   </i> <br>  test case </p> <br> <br> <br>", "test case")]
        [TestCase("<p> </p> <p>  <i>   </i> <br>  test case<br><br> </p> <br> <br> <br>", "test case")]
        public void ParseBreak_TextAfterBreak_ReturnDocumentWithTwoRows(string html, string textAfterBreak)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(contentText: string.Empty)
                .AppendRow().AppendBlock().AppendContent(textAfterBreak)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase(" test case <br> 12312awqaw", "test case", "12312awqaw")]
        [TestCase(" <p>  <i> </i> </p> test case <br> <p>  <i> </i> </p> 12312awqaw <p>  <i> </i> </p> ", "test case", "12312awqaw")]
        [TestCase(" <p>  <i> </i> </p> test case <p> <br> </p> <p>  <i> </i> </p> 12312awqaw <p>  <i> </i> </p> ", "test case", "12312awqaw")]
        public void ParseBreak_TextBeforeAndAfterBreak_ReturnDocumentWithTwoRows
            (string html, string textBeforeBreak, string textAfterBreak)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(textBeforeBreak)
                .AppendRow().AppendBlock().AppendContent(textAfterBreak)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }


        [TestCase(" test case <br> <br> 12312awqaw", "test case", "12312awqaw")]
        [TestCase(" test case <br> <p> <p> </p> </p> <br> 12312awqaw", "test case", "12312awqaw")]
        [TestCase(" test case <br> <p> <p> </p>     </p> <br> <p>12312awqaw </p>", "test case", "12312awqaw")]
        [TestCase(@" test case <br> <p> <p> </p>  
                     </p> <br> <p>12312awqaw </p>", "test case", "12312awqaw")]
        public void ParseBreak_TextBeforeAndAfterDoblueBreak_ReturnDocumentWithThreeRows
            (string html, string textBeforeBreak, string textAfterBreak)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(textBeforeBreak)
                .AppendRow().AppendBlock().AppendContent(contentText: string.Empty)
                .AppendRow().AppendBlock().AppendContent(textAfterBreak)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase(" test case <br>  <br> <br> 12312awqaw", "test case", "12312awqaw")]
        [TestCase(" test case <br> <p> <p> </p> </p> <br> <p> </p> <p> </p> <p> </p> <br> 12312awqaw", "test case", "12312awqaw")]
        [TestCase(" <p> test case <br> </p>  <p> <p> </p> </p> <br> <p> </p> <p> </p> <p> </p> <br> 12312awqaw", "test case", "12312awqaw")]
        public void ParseBreak_TextBeforeAndAfterTripleBreak_ReturnDocumentWithFourRows
            (string html, string textBeforeBreak, string textAfterBreak)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(textBeforeBreak)
                .AppendRow().AppendBlock().AppendContent(contentText: string.Empty)
                .AppendRow().AppendBlock().AppendContent(contentText: string.Empty)
                .AppendRow().AppendBlock().AppendContent(textAfterBreak)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        [TestCase(" test case <br> sourcre  <br> 5454ras <br> 12312awqaw", "test case", "sourcre", "5454ras", "12312awqaw")]
        [TestCase(" <p> </p> test case  <br> sourcre <br> 5454ras <br> 12312awqaw", "test case", "sourcre", "5454ras", "12312awqaw")]
        public void ParseBreak_TextBetweenTripleBreak_ReturnDocumentWithFourRows
            (string html, string firstText, string secondText, string thirdText, string forthText)
        {
            // Arrange
            var sut = CreateSut();
            var expectedDocument = new Document()
                .AppendRow().AppendBlock().AppendContent(firstText)
                .AppendRow().AppendBlock().AppendContent(secondText)
                .AppendRow().AppendBlock().AppendContent(thirdText)
                .AppendRow().AppendBlock().AppendContent(forthText)
                .ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
