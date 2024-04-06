namespace RedLabel.HtmlParsing.Core.Models
{
    public class RowStyle
    {
        public bool IsThematicBreakRow { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public int IndentationCoefficient { get; set; }

        public RowStyle(RowStyle rowStyle)
        {
            SetProperties(rowStyle.IsThematicBreakRow, rowStyle.HorizontalAlignment
                        , rowStyle.IndentationCoefficient);
        }

        public RowStyle(bool isThematicBreakRow, HorizontalAlignment horizontalAlignment
                      , int indentationCoefficient)
        {
            SetProperties(isThematicBreakRow, horizontalAlignment, indentationCoefficient);
        }

        private void SetProperties(bool isThematicBreakRow
            , HorizontalAlignment horizontalAlignment, int indentationCoefficient)
        {
            this.IsThematicBreakRow = isThematicBreakRow;
            this.HorizontalAlignment = horizontalAlignment;
            this.IndentationCoefficient = indentationCoefficient;
        }
    }
}
