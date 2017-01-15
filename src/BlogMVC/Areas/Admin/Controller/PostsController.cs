using BlogMVC.Controllers;
using BlogMVC.Data;
using BlogMVC.Models;
using BlogMVC.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Area.Posts.Controller
{
    [Authorize]
    [Area("Admin")]
    public class PostsController : BaseController
    {
        private readonly BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int startIndex = page <= 1 ? 0 : (page - 1) * ITEM_PER_PAGE;

            List<Post> posts = await _context.Posts
                                             .Include(p => p.Category)
                                             .Include(p => p.User)
                                             .OrderByDescending(p => p.Created)
                                             .Skip(startIndex)
                                             .Take(ITEM_PER_PAGE)
                                             .ToListAsync();

            return View(new PagedResult<Post>() { CurrentPage = page, PageCount = await _context.Posts.CountAsync(), Results = posts });
        }

        public async Task<IActionResult> Delete(int id)
        {
            Post post = await _context.Posts.Include(p => p.Comments).SingleOrDefaultAsync(p => p.Id == id);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Posts", new { area = "Admin" });
        }
    }
}
