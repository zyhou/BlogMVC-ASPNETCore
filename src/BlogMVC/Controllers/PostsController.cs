using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogMVC.Data;
using Microsoft.EntityFrameworkCore;
using BlogMVC.Models;
using BlogMVC.Models.Paging;
using Microsoft.AspNetCore.Authorization;

namespace BlogMVC.Controllers
{
    [AllowAnonymous]
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

        [HttpGet("/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            Post post = await _context.Posts
                                     .Include(p => p.Comments)
                                     .Include(p => p.Category)
                                     .Include(p => p.User)
                                     .FirstOrDefaultAsync(p => p.Slug == slug);

            if (post == null) return View("Error");

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(Comment newComment)
        {
            Post post = await _context.Posts
                                     .Include(p => p.Comments)
                                     .Include(p => p.Category)
                                     .Include(p => p.User)
                                     .SingleOrDefaultAsync(p => p.Id == newComment.IdPost);

            if (post == null) return View("Error");

            if (!ModelState.IsValid)
            {
                return View("Details", post);
            }

            newComment.Created = DateTime.Now;
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { slug = post.Slug });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
