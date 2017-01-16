using BlogMVC.Controllers;
using BlogMVC.Data;
using BlogMVC.Models;
using BlogMVC.Models.Paging;
using BlogMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Create()
        {
            var postVm = new PostViewModel();
            postVm.CategoriesSelected = await _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToListAsync();
            postVm.UsersSelected = await _context.Users.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.UserName }).ToListAsync();
            return View(postVm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postVm)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post(postVm);
                post.Created = DateTime.Now;
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync(); ;

                return RedirectToAction("Index", "Posts", new { area = "Admin" });
            }

            postVm.CategoriesSelected = await _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToListAsync();
            postVm.UsersSelected = await _context.Users.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.UserName }).ToListAsync();

            return View(postVm);
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
