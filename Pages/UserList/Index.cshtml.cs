using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UsersApp.Model;

namespace UsersApp.Pages.UserList
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> Users { get; set; }
        public async Task OnGet()
        {

            Users = await _db.User.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var user = await _db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsDeleted = true;
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
