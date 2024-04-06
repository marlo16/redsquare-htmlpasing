using HtmlAgilityPack;

namespace RedLabel.HtmlParsing.Core
{
    public static class HtmlNodeExtensions
    {
        public static string GetFormattedName(this HtmlNode htmlNode) 
            => htmlNode.Name.ToLower().Trim();
    }
}
