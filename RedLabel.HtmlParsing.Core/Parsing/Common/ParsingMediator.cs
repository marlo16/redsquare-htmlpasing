using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    internal class ParsingMediator : IParsingMediator
    {
        private readonly List<IParsingMiddleware> _middlewares = new List<IParsingMiddleware>();

        public Document Parse(HtmlDocument document)
        {
            var parsedDocument = new Document();
            var parsingContext = new ParsingContext(parsedDocument);
            Parse(document.DocumentNode, parsingContext);
            return parsedDocument;
        }

        public void Parse(HtmlNode node, ParsingContext context)
        {
            var applyingMiddleware = _middlewares
                .Where(m => m.ShouldApply(node, context));
            foreach (var middleware in applyingMiddleware)
            {
                middleware.Parse(node, context);
            }
        }

        public void RegisterMiddleware(IParsingMiddleware parsingMiddleware)
        {
            parsingMiddleware.SetParsingMediator(this);
            _middlewares.Add(parsingMiddleware);
        }
    }
}
