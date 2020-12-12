using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UsersApp.Model;

namespace FromTutorial.Pages.UserList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task OnGet(int id)
        {
            User = await _db.User.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var UserFromDb = await _db.User.FindAsync(User.Id);
                UserFromDb.Name = User.Name;
                UserFromDb.Surname = User.Surname;
                UserFromDb.Login = User.Login;
                UserFromDb.DateOfBirth = User.DateOfBirth;
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
