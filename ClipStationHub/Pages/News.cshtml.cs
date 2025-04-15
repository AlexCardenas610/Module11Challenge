using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClipStationHub.Pages
{
    public class NewsModel : PageModel
    {
        public List<NewsArticle> Articles { get; set; }

        public void OnGet()
        {
            Articles = new List<NewsArticle>
            {
                new NewsArticle { Headline = "New Features Released!", Content = "Discover the latest updates in ClipStationHub." },
                new NewsArticle { Headline = "ClipStationHub Community Grows", Content = "More creators are joining every day." }
            };
        }
    }

    public class NewsArticle
    {
        public string Headline { get; set; }
        public string Content { get; set; }
    }
}
