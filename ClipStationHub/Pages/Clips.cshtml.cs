using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;

namespace ClipStationHub.Pages
{
    public class ClipsModel : PageModel
    {
        // List to store uploaded file paths
        public List<string> DisplayedFiles { get; set; }

        public void OnGet()
        {
            // Get the path to the uploads folder
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            // Ensure the uploads folder exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Get all video files in the uploads folder
            var videoFiles = Directory.GetFiles(uploadsFolder, "*.*", SearchOption.TopDirectoryOnly);

            // Filter for video file extensions (e.g., .mp4, .avi, .mkv)
            var allowedExtensions = new[] { ".mp4", ".avi", ".mkv", ".webm" };
            DisplayedFiles = new List<string>();

            foreach (var file in videoFiles)
            {
                if (allowedExtensions.Contains(Path.GetExtension(file).ToLower()))
                {
                    // Convert the file path to a relative URL
                    var relativePath = "/uploads/" + Path.GetFileName(file);
                    DisplayedFiles.Add(relativePath);
                }
            }
        }
    }
}