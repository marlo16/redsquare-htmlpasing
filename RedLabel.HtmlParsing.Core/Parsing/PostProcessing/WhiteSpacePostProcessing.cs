using RedLabel.HtmlParsing.Core.Models;
using System.Text.RegularExpressions;

namespace RedLabel.HtmlParsing.Core
{
    public class WhiteSpacePostProcessing : IDocumentPostProcessing
    {
        private const string REPLACE_PATTERN = @"\s+";
        private const string SINGLE_WHITESPACE = " ";

        public void ExecuteProcessing(Document document)
        {
            foreach (var blockRow in document.Rows)
            {
                foreach (var blockElement in blockRow.Elements)
                {
                    TrimContent(blockElement);
                }
            }
        }

        private static void TrimContent(BlockElement blockElement)
        {
            if (blockElement.Contents.Count == 0)
            {
                return;
            }

            BlockElementContent previousContent = null;
            foreach (var content in blockElement.Contents)
            {
                if (content.ContentStyle.IsPreformattedText)
                {
                    continue;
                }

                content.Content = Regex.Replace(content.Content, REPLACE_PATTERN, SINGLE_WHITESPACE);
                if (previousContent is null)
                {
                    content.Content = content.Content.TrimStart();
                    previousContent = content;
                    continue;
                }

                TrimContent(content, previousContent);
                previousContent = content;
            }

            if (previousContent != null)
            {
                previousContent.Content = previousContent.Content.TrimEnd();
            }
        }

        private static void TrimContent(BlockElementContent currentContent
                                      , BlockElementContent previousContent)
        {
            if (currentContent.Content.Length == 0)
            {
                return;
            }
            var isFirstCharWhiteSpace = char.IsWhiteSpace(currentContent.Content[0]);

            if (previousContent.Content.Length == 0)
            {
                return;
            }
            var previousContentLastIndex = previousContent.Content.Length - 1;
            var lastCharWhiteSpace = char.IsWhiteSpace(previousContent.Content[previousContentLastIndex]);
            if (isFirstCharWhiteSpace && lastCharWhiteSpace)
            {
                currentContent.Content = currentContent.Content.TrimStart();
            }
        }
    }
}
