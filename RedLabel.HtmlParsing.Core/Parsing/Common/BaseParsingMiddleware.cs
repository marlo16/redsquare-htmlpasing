using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace RedLabel.HtmlParsing.Core
{
    public class BaseParsingMiddleware : IParsingMiddleware
    {
        protected IParsingMediator _parsingMediator;

        public virtual void Parse(HtmlNode node, ParsingContext context)
        {
            if (node is null || node.ChildNodes is null)
            {
                return;
            }

            foreach (var childNode in node.ChildNodes)
            {
                _parsingMediator.Parse(childNode, context);
            }
        }

        public virtual bool ShouldApply(HtmlNode htmlNode, ParsingContext context)
        {
            return false;
        }

        public void SetParsingMediator(IParsingMediator parsingMediator)
        {
            this._parsingMediator = parsingMediator;
        }

        protected HtmlNode GetPreviousNotEmptyNode(HtmlNode node, ParsingContext context)
        {
            if (node is null)
            {
                return node;
            }

            return IsEmptyNode(node.PreviousSibling, context)
                ? GetNextNotEmptyNode(node.PreviousSibling, context)
                : node.PreviousSibling;
        }

        protected HtmlNode GetNextNotEmptyNode(HtmlNode node, ParsingContext context)
        {
            if (node is null)
            {
                return node;
            }

            return IsEmptyNode(node.NextSibling, context)
                ? GetNextNotEmptyNode(node.NextSibling, context)
                : node.NextSibling;
        }

        protected bool HasNotEmptyChildNode(HtmlNode node, ParsingContext context)
        {

            if (node.ChildNodes is null || !node.ChildNodes.Any())
            {
                return false;
            }

            return node.ChildNodes.Any(n => !IsEmptyNode(n, context))
                || node.ChildNodes.Any(n => HasNotEmptyChildNode(n, context));
        }

        protected bool IsEmptyNode(HtmlNode node, ParsingContext context)
        {
            if (node is null)
            {
                return true;
            }

            if (context.CurrentContentStyle.IsPreformattedText)
            {
                return false;
            }

            var newlinePattern = new Regex("^(\r\n|\r|\n)*$");
            var whiteSpacePattern = new Regex(@"^\s+$");
            return newlinePattern.IsMatch(node.InnerText) 
                || whiteSpacePattern.IsMatch(node.InnerText);
        }

        protected void AppendNewRow(ParsingContext context)
        {
            var parentDocument = context.CurrentDocument;

            var rowStyle = new RowStyle(context.CurrentRowStyle);
            var newBlockRow = new BlockRow(parentDocument, rowStyle);
            var newBlockElement = new BlockElement(newBlockRow);
            newBlockRow.Elements.Add(newBlockElement);
            parentDocument.Rows.Add(newBlockRow);
            context.CurrentBlockElement = newBlockElement;
        }
    }
}
