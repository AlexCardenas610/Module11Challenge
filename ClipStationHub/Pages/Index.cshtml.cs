
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

public class IndexModel : PageModel
{
    public List<string> UploadedFiles { get; set; }

    public void OnGet()
    {
        // Retrieve the list of uploaded files
        UploadedFiles = UploadModel.UploadedFiles;
    }
}