using RedLabel.HtmlParsing.Core.Models;
using System.Linq;

namespace RedLabel.HtmlParsing.Core
{
    public class ParsingContext
    {
        public Document CurrentDocument { get; set; }
        public TextStyle CurrentContentStyle { get; set; }
        public RowStyle CurrentRowStyle { get; set; }
        public BlockElement CurrentBlockElement { get; set; }

        public bool IsCurrentRowEmpty 
        {
            get => this.CurrentBlockElement != null
                && !this.CurrentBlockElement.Contents.Any()
                && !this.CurrentBlockElement.ParentRow.RowStyle.IsThematicBreakRow; 
        }

        public ParsingContext(Document currentDocument)
        {
            CurrentDocument = currentDocument;
        }

        public void SetRowStyle(RowStyle rowStyle)
        {
            this.CurrentRowStyle = rowStyle;
        }

        public void SetContentStyle(TextStyle currentContentStyle)
        {
            this.CurrentContentStyle = currentContentStyle;
        }

        public void SetBlockElement(BlockElement blockElement)
        {
            this.CurrentBlockElement = blockElement;
        }
    }
}
