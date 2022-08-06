using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Text;

namespace Portfolio.Areas.News.Components
{
    public class PostViewComponent : ViewComponent
    {
        private const string h1OpenTag = "[h1]";
        private const string h1CloseTag = "[/h1]";

        public IViewComponentResult Invoke(string content)
        {
            content = content.Replace("<", "&lt").Replace(">", "&gt");

            content = content.Replace("[br]", "<br>");

            var sb = new StringBuilder();
            sb.Append("<div>");

            do
            {
                int openIndex = content.IndexOf(h1OpenTag);
                int closeIndex = content.IndexOf(h1CloseTag);

                if (openIndex == -1 || closeIndex == -1)
                {
                    sb.Append(content);
                    content = "";
                }
                else
                {
                    sb.Append(content.Substring(0, openIndex));
                    sb.Append($"<h1>{content.Substring(openIndex + h1OpenTag.Length, closeIndex - openIndex - h1OpenTag.Length)}</h1>");
                    content = content.Substring(closeIndex + h1CloseTag.Length);
                }
            } while (content != "");
            sb.Append("</div>");

            return new HtmlContentViewComponentResult(new HtmlString(sb.ToString()));
        }
    }
}
