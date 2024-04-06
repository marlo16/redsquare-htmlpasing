namespace RedLabel.HtmlParsing.Core.Models
{
    public class BlockElementContent
    {
        public string Content { get; set; }
        public TextStyle ContentStyle { get; set; }
        public BlockElement ParentBlockElement { get; set; }
        public bool IsBarElementContent { get; set; }

        public BlockElementContent(string content, TextStyle contentStyle
            , BlockElement parentBlockElement, bool isBarElementContent = false)
        {
            this.Content = content ?? string.Empty;
            this.ContentStyle = new TextStyle(contentStyle);
            this.ParentBlockElement = parentBlockElement;
            this.IsBarElementContent = isBarElementContent;
        }
    }
}
