using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly BlogContext _context;

        public UsersController(BlogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var a = await _context.Users.Include(u => u.Posts).ToListAsync();
            return View(a);
        }

        public async Task<IActionResult> Index1()
        {
            var a = await _context.Posts.Include(u => u.Comments).Include(u => u.Category).ToListAsync();
            return View(a);
        }

        public async Task<IActionResult> Index2()
        {
            return View(await _context.Comments.ToListAsync());
        }

        public async Task<IActionResult> Index3()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}
