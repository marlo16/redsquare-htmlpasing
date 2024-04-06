using RedLabel.HtmlParsing.Core.Models;
using RedLabel.HtmlParsing.Core.Tests.Extensions;

namespace RedLabel.HtmlParsing.Core.Tests.ParsingMiddlewareTests
{
    [TestFixture]
    public class SuperscriptParsingMiddlewareTests
    {
        [TestCase("This text contains <sup>subscript</sup> text.", "This text contains ", "subscript", " text.")]
        [TestCase("This text contains <sup><sup>subscript</sup></sup> text.", "This text contains ", "subscript", " text.")]
        [TestCase("This text contains <sup><sub><sup>subscript</sup></sub></sup> text.", "This text contains ", "subscript", " text.")]
        public void ParseSuperscript_ThreeContentsWithSingleSupperScript_ReturnRowWithSingleSuperscriptStyle
            (string html, string firstExpectedContent, string secondSupperscriptContent, string thirdExpectedContent)
        {
            // Arrange
            var sut = CreateSut();
            var expectedSuperscriptContentStyle = CreationRowHelper.CreateTextStyle(isSuperscript: true);
            var expectedDocument = new Document().AppendRow().AppendBlock()
                    .AppendContent(firstExpectedContent)
                    .AppendContent(secondSupperscriptContent, expectedSuperscriptContentStyle)
                    .AppendContent(thirdExpectedContent).ParentRow.ParentDocument;

            // Act
            var result = sut.ParseHtml(html);

            // Assert
            result.AssertThatEqual(expectedDocument);
        }

        private static HtmlParser CreateSut() => new();
    }
}
