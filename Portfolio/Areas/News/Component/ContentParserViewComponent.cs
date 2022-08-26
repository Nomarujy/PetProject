using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Portfolio.Areas.News.Component
{
    public class ContentParser : ViewComponent
    {
        public HtmlContentViewComponentResult Invoke(string content)
        {
            var doubleTags = new DoubleTag[]
            {
                new DoubleTag("h1"),
                new DoubleTag("p"),
                new DoubleTag("li")
            };

            UpdateDoubleTags(ref content, doubleTags);

            var singleTags = new SingleTag[]
            {
                new SingleTag("br")
            };

            UpdateSignleTags(ref content, singleTags);

            return new HtmlContentViewComponentResult(new HtmlString(content));
        }

        private void UpdateDoubleTags(ref string content, DoubleTag[] tags)
        {
            foreach (var tag in tags)
            {
                bool TagsFinded = true;
                do
                {
                    int openIndex = content.IndexOf(tag.Open);
                    int closeIndex = content.IndexOf(tag.Close);

                    if (openIndex != -1 && closeIndex != -1)
                    {
                        content = content.Remove(openIndex, 1).Insert(openIndex, "<");
                        int BackIdex = openIndex + tag.Open.Length - 1;
                        content = content.Remove(BackIdex, 1).Insert(BackIdex, ">");

                        content = content.Remove(closeIndex, 1).Insert(closeIndex, "<");
                        BackIdex = closeIndex + tag.Close.Length - 1;
                        content = content.Remove(BackIdex, 1).Insert(BackIdex, ">");
                    }
                    else
                    {
                        TagsFinded = false;
                    }
                } while (TagsFinded);
            }
        }

        private void UpdateSignleTags(ref string content, SingleTag[] tags)
        {
            foreach (var item in tags)
            {
                content = content.Replace(item.Tag, item.NewTag);
            }
        }

        public class DoubleTag
        {
            public DoubleTag(string Tag)
            {
                Open = $"[{Tag}]";
                Close = $"[/{Tag}]";
            }

            public string Open { get; private set; }
            public string Close { get; private set; }
        }

        public class SingleTag
        {
            public SingleTag(string tag)
            {
                Tag = $"[{tag}]";
                NewTag = $"<{tag}>";
            }

            public string Tag { get; private set; }
            public string NewTag { get; private set; }
        }

    }
}
