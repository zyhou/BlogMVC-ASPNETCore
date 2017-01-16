using BlogMVC.Data;
using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.ViewComponents
{
    public class PostsList : ViewComponent
    {
        private readonly BlogContext _context;

        public PostsList(BlogContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            var items = await GetItemsAsync(take);
            return View(items);
        }

        private Task<List<Post>> GetItemsAsync(int take)
        {
            return _context.Posts.OrderByDescending(p => p.Created).Take(take).ToListAsync();
        }
    }
}
