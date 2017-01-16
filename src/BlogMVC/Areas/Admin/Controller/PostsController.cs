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
            await postVm.InitListSeleted(_context);
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

            await postVm.InitListSeleted(_context);

            return View(postVm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Post post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);

            var postVm = new PostViewModel(post);
            await postVm.InitListSeleted(_context);

            return View("Create", postVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel postVm)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post(postVm);
                _context.Entry(post).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Posts", new { area = "Admin" });
            }

            await postVm.InitListSeleted(_context);

            return View("Create", postVm);
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
