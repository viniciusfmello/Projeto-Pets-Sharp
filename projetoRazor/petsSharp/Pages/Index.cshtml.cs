using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace petsSharp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Senha { get; set; }

        public bool mensagemLogin = false;

        public IndexModel()
        {
           
        }

        public void OnGet()
        {
           
        }
        public IActionResult OnPost() {
            if (Username == "admin" && Senha == "12345") {
                return RedirectToPage("/dashboard");
            }
            mensagemLogin = true;
            return Page();
            
        }


    }
}