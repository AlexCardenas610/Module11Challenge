using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    [BindProperty]
    public UserModel UserModel { get; set; }

    public string Message { get; set; }

    public void OnGet()
    {
        // Initialize any data if needed
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page(); // Return the page with validation errors
        }

        // Process the form data (e.g., save to database, send email, etc.)
        Message = "Thank you for joining the community!";
        return Page();
    }
}