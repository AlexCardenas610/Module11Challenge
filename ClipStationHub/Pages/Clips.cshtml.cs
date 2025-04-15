using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[RequestSizeLimit(104857600)] // 100 MB
public class UploadModel : PageModel
{
    private const long MaxFileSize = 104857600; // 100 MB

    // Static list to store uploaded file paths
    public static List<string> UploadedFiles { get; set; } = new List<string>();

    public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
    {
        if (file == null)
        {
            ModelState.AddModelError("File", "No file uploaded.");
            return Page();
        }

        // Validate file size
        if (file.Length > MaxFileSize)
        {
            ModelState.AddModelError("File", "File is too large. Max size is 100 MB.");
            return Page();
        }

        // Validate file type (ensure it's a video)
        var allowedFileTypes = new[] { "video/mp4", "video/avi", "video/mkv", "video/webm" };
        if (!allowedFileTypes.Contains(file.ContentType))
        {
            ModelState.AddModelError("File", "Invalid file type. Please upload a video file.");
            return Page();
        }

        // Validate file extension
        var allowedExtensions = new[] { ".mp4", ".avi", ".mkv", ".webm" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
        {
            ModelState.AddModelError("File", "Invalid file extension. Only video files are allowed.");
            return Page();
        }

        // Save the file to the server
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);

        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads"));
        }

        using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Add the file path to the static list
        UploadedFiles.Add($"/uploads/{file.FileName}");

        return RedirectToPage("/Index"); // Redirect to the home page
    }
}