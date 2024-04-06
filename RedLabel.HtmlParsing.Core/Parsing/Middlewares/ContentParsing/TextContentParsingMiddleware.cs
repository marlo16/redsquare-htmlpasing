using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;
using System;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class TextContentParsingMiddleware : BaseParsingMiddleware
    {
        public override void Parse(HtmlNode htmlNode, ParsingContext context)
        {
            if (context.CurrentBlockElement is null)
            {
                AppendNewRow(context);
            }

            if (context.CurrentContentStyle.IsPreformattedText)
            {
                ParsePreformattedContent(htmlNode, context);
            }
            else
            {
                ParseContent(htmlNode.InnerText, context);
            }

            base.Parse(htmlNode, context);
        }

        private void ParsePreformattedContent(HtmlNode htmlNode, ParsingContext context)
        {
            var separators = new string[] { Environment.NewLine };
            var splittedContents = htmlNode.InnerText.Split(separators, StringSplitOptions.None);
            const int FIRST_ELEMENT_INDEX = 0;
            for (var i = FIRST_ELEMENT_INDEX; i < splittedContents.Length; i++)
            {
                if (i == FIRST_ELEMENT_INDEX 
                 && string.IsNullOrEmpty(splittedContents[i]))
                {
                    continue;
                }

                if (context.CurrentBlockElement.Contents.Count != 0)
                {
                    AppendNewRow(context);
                }

                ParseContent(splittedContents[i], context);
            }
        }

        private void ParseContent(string innerText, ParsingContext context)
        {
            if (context.CurrentContentStyle.TextDirectionType == TextDirectionType.RightToLeft)
            {
                var reversedContentChars = innerText.Reverse().ToArray();
                innerText = new string(reversedContentChars);
            }

            var content = new BlockElementContent(innerText, context.CurrentContentStyle
                                                , context.CurrentBlockElement);
            context.CurrentBlockElement.Contents.Add(content);
        }

        public override bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return htmlNode.GetFormattedName() == "#text" 
               && !IsEmptyNode(htmlNode, context);
        }
    }
}
