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
    public class IndexModel : PageModel
    {
        private readonly JWTAuth.WebAPI.Models.DatabaseContext _context;

        public IndexModel(JWTAuth.WebAPI.Models.DatabaseContext context)
        {
            _context = context;
        }

        public IList<UserModel> UserModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                UserModel = await _context.Users.ToListAsync();
            }
        }
    }
}
