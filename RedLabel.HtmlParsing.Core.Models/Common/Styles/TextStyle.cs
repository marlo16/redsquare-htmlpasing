namespace RedLabel.HtmlParsing.Core.Models
{
    public class TextStyle
    {
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool HasUnderLine { get; set; }
        public bool IsStrikeout { get; set; }
        public bool IsSuperscript { get; set; }
        public bool IsSubrscript { get; set; }
        public float FontSize { get; set; }
        public string FontFamilyName { get; set; }
        public string FontColor { get; set; }
        public string BackgroundColor { get; set; }
        public string Title { get; set; }
        public TextDirectionType TextDirectionType { get; set; }

        public bool IsPreformattedText { get; set; }

        public bool IsHyperlink { get => !string.IsNullOrEmpty(this.Hyperlink); }
        public string Hyperlink { get; set; }

        public TextStyle(TextStyle textStyle)
        {
            SetProperties(textStyle.IsBold, textStyle.IsItalic, textStyle.HasUnderLine
                        , textStyle.IsStrikeout, textStyle.FontSize, textStyle.FontFamilyName
                        , textStyle.FontColor, textStyle.Title, textStyle.Hyperlink
                        , textStyle.TextDirectionType, textStyle.BackgroundColor
                        , textStyle.IsPreformattedText, textStyle.IsSuperscript
                        , textStyle.IsSubrscript);
        }

        public TextStyle(bool isBold, bool isItalic, bool hasUnderLine
            , bool isStrikeout, float fontSize, string fontFamilyName, string fontColor
            , string title, string hyperlink, TextDirectionType textDirectionType
            , string backgroundColor, bool isPreformattedText, bool isSuperscript
            , bool isSubrscript)
        {
            SetProperties(isBold, isItalic, hasUnderLine, isStrikeout
                        , fontSize, fontFamilyName, fontColor, title, hyperlink
                        , textDirectionType, backgroundColor, isPreformattedText
                        , isSuperscript, isSubrscript);
        }
        
        private void SetProperties(bool isBold, bool isItalic, bool hasUnderLine
            , bool isStrikeout, float fontSize, string fontFamilyName, string fontColor
            , string title, string hyperlink, TextDirectionType textDirectionType
            , string backgroundColor, bool isPreformattedText, bool isSuperscript
            , bool isSubrscript)
        {
            this.IsBold = isBold;
            this.IsItalic = isItalic;
            this.HasUnderLine = hasUnderLine;
            this.IsStrikeout = isStrikeout;
            this.FontSize = fontSize;
            this.FontFamilyName = fontFamilyName;
            this.FontColor = fontColor;
            this.Title = title;
            this.Hyperlink = hyperlink;
            this.TextDirectionType = textDirectionType;
            this.BackgroundColor = backgroundColor;
            this.IsPreformattedText = isPreformattedText;
            this.IsSuperscript = isSuperscript;
            this.IsSubrscript = isSubrscript;
        }
    }
}
