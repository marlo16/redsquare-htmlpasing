using System.Collections.Generic;

namespace RedLabel.HtmlParsing.Core.Models
{
    public class BlockElement
    {
        public int Index { get; set; }
        public BlockElementSpan Span { get; internal set; } = new BlockElementSpan();
        public BlockElementStyle Style { get; internal set; } = new BlockElementStyle();
        public BlockElementBorder Borders { get; internal set; } = new BlockElementBorder();
        public BlockElementSize Size { get; internal set; } = new BlockElementSize();
        public List<BlockElementContent> Contents { get; internal set; } = new List<BlockElementContent>();
        public BlockRow ParentRow { get; set; }

        public BlockElement(BlockRow parentRow)
        {
            this.ParentRow = parentRow;
        }
    }
}
