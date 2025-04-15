using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClipStationHub.Pages
{
    public class GuidesModel : PageModel
    {
        public List<Guide> Guides { get; set; }

        public void OnGet()
        {
            Guides = new List<Guide>
            {
                new Guide { Title = "How to Create Stunning Clips", Steps = "Step-by-step instructions for making engaging clips." },
                new Guide { Title = "Using ClipStationHub Efficiently", Steps = "Maximizing productivity within the platform." }
            };
        }
    }

    public class Guide
    {
        public string Title { get; set; }
        public string Steps { get; set; }
    }
}
