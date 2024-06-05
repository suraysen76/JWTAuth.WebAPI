using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JWTAuth.WebAPI.Models;

namespace JWTAuth.WebAPI.Views.User
{
    public class DetailsModel : PageModel
    {
        private readonly JWTAuth.WebAPI.Models.DatabaseContext _context;

        public DetailsModel(JWTAuth.WebAPI.Models.DatabaseContext context)
        {
            _context = context;
        }

      public UserModel UserModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var usermodel = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (usermodel == null)
            {
                return NotFound();
            }
            else 
            {
                UserModel = usermodel;
            }
            return Page();
        }
    }
}
