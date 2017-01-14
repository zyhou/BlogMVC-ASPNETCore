﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogMVC.Data;
using Microsoft.EntityFrameworkCore;
using BlogMVC.Models;
using BlogMVC.Models.Paging;

namespace BlogMVC.Controllers
{
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
