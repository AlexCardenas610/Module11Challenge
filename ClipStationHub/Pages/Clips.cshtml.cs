using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace ClipStationHub.Pages
{
    public class ClipsModel : PageModel
    {
        [BindProperty]
        public IFormFile ClipFile { get; set; }

        public async Task<IActionResult> OnPostUploadClip()
{
    if (ClipFile == null || ClipFile.Length == 0)
    {
        ModelState.AddModelError("", "Invalid file. Please select a valid video clip.");
        return Page();
    }

    // Set a max file size (100MB in bytes)
    const long maxFileSize = 104857600;

    if (ClipFile.Length > maxFileSize)
    {
        ModelState.AddModelError("", "File size exceeds the 100MB limit.");
        return Page();
    }

    // Define the target directory where clips will be saved
    var uploadsFolder = Path.Combine("wwwroot", "clips");
    Directory.CreateDirectory(uploadsFolder);

    var filePath = Path.Combine(uploadsFolder, ClipFile.FileName);

    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
        await ClipFile.CopyToAsync(fileStream);
    }

    return RedirectToPage("/Clips");
}
        public void OnGet()
        {

        }
    }
}
        

