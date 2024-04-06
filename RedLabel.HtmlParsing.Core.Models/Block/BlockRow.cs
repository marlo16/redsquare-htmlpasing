using System.Collections.Generic;

namespace RedLabel.HtmlParsing.Core.Models
{
    public class BlockRow
    {
        public int Index { get; set; }
        public List<BlockElement> Elements { get; set; } = new List<BlockElement>();
        public Document ParentDocument { get; set; }
        public RowStyle RowStyle { get; set; }

        public BlockRow(Document parentDocument, RowStyle rowStyle)
        {
            this.ParentDocument = parentDocument;
            this.RowStyle = rowStyle;
        }
    }
}
