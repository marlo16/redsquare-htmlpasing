using HtmlAgilityPack;
using RedLabel.HtmlParsing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class HtmlParser
    {
        private IParsingMediator _parsingMediator;
        private readonly List<IDocumentPostProcessing> _postProcessings 
            = new List<IDocumentPostProcessing>();

        public HtmlParser(IEnumerable<IParsingMiddleware> middlewares
                    , IEnumerable<IDocumentPostProcessing> postProcessings)
        {
            InitParsingMediator();
            foreach (var middleware in middlewares)
            {
                RegisterMiddleware(middleware);
            }

            if (postProcessings != null && postProcessings.Any())
            {
                _postProcessings.AddRange(postProcessings);
            }
        }

        public void RegisterMiddleware(IParsingMiddleware middleware)
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            _parsingMediator.RegisterMiddleware(middleware);
        }

        public HtmlParser()
        {
            InitParsingMediator();
            InitDefaultParsingMiddleware();
            InitDefaultPostProcessings();
        }

        public Document ParseHtml(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var parsedDocument = _parsingMediator.Parse(htmlDocument);

            _postProcessings.ForEach(p => p.ExecuteProcessing(parsedDocument));

            return parsedDocument;
        }

        private void InitDefaultPostProcessings()
        {
            _postProcessings.Add(new WhiteSpacePostProcessing());
        }

        private void InitDefaultParsingMiddleware()
        {
            this.FleuntRegisterMiddleware(new HtmlParsingMiddleware())
                .FleuntRegisterMiddleware(new HeadParsingMiddleware())
                .FleuntRegisterMiddleware(new DocumentParsingMiddleware())
                .FleuntRegisterMiddleware(new ParagraphParsingMiddleware())
                .FleuntRegisterMiddleware(new BreakParsingMiddleware())
                .FleuntRegisterMiddleware(new StrikeoutParsingMiddleware())
                .FleuntRegisterMiddleware(new TextContentParsingMiddleware())
                .FleuntRegisterMiddleware(new ItalicParsingMiddleware())
                .FleuntRegisterMiddleware(new BoldParsingMiddleware())
                .FleuntRegisterMiddleware(new UnderlineParsingMiddleware())
                .FleuntRegisterMiddleware(new ShortQuotationParsingMiddleware())
                .FleuntRegisterMiddleware(new AbbreviationParsingMiddleware())
                .FleuntRegisterMiddleware(new HyperlinkParsingMiddleware())
                .FleuntRegisterMiddleware(new HeadingsParsingMiddleware())
                .FleuntRegisterMiddleware(new ThematicBreakParsingMiddleware())
                .FleuntRegisterMiddleware(new AddressParsingMiddleware())
                .FleuntRegisterMiddleware(new BiDirectionParsingMiddleware())
                .FleuntRegisterMiddleware(new BlockQuotationParsingMiddleware())
                .FleuntRegisterMiddleware(new CenterParsingMiddleware())
                .FleuntRegisterMiddleware(new FontParsingMiddleware())
                .FleuntRegisterMiddleware(new MarkParsingMiddleware())
                .FleuntRegisterMiddleware(new MeterParsingMiddleware())
                .FleuntRegisterMiddleware(new PreformattedParsingMiddleware())
                .FleuntRegisterMiddleware(new ProgressParsingMiddleware())
                .FleuntRegisterMiddleware(new TitleParsingMiddleware())
                .FleuntRegisterMiddleware(new SuperscriptParsingMiddleware())
                .FleuntRegisterMiddleware(new SubscriptParsingMiddleware())
                .FleuntRegisterMiddleware(new TimeParsingMiddleware());
        }

        private void InitParsingMediator()
        {
            _parsingMediator = new ParsingMediator();
        }
    }
}
