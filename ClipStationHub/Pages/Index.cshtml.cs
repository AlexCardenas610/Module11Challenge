using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using ClipStationHub.Pages; // Add this namespace to access ClipsModel

public class IndexModel : PageModel
{
    public List<string> UploadedFiles { get; set; }

    public void OnGet()
    {
        // Retrieve the list of displayed files from ClipsModel
        var clipsModel = new ClipsModel();
        clipsModel.OnGet(); // Populate the DisplayedFiles property
        UploadedFiles = clipsModel.DisplayedFiles;
    }
}