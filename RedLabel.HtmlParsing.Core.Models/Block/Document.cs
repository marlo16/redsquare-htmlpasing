using System.Collections.Generic;

namespace RedLabel.HtmlParsing.Core.Models
{
    public class Document
    {
        public string Title { get; set; }
        public List<BlockRow> Rows { get; set; } = new List<BlockRow>();
    }
}
