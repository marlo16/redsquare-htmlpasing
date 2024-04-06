using RedLabel.HtmlParsing.Core.Models;

namespace RedLabel.HtmlParsing.Core
{
    public interface IDocumentPostProcessing
    {
        void ExecuteProcessing(Document document);
    }
}
