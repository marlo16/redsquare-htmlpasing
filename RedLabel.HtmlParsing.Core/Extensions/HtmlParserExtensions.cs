using System;

namespace RedLabel.HtmlParsing.Core
{
    public static class HtmlParserExtensions
    {
        public static HtmlParser FleuntRegisterMiddleware
            (this HtmlParser parser, IParsingMiddleware middleware)
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            parser.RegisterMiddleware(middleware);
            return parser;
        }
    }
}
