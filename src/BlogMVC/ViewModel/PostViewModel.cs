using BlogMVC.Data;
using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.ViewModel
{
    public class PostViewModel : Post
    {
        public List<SelectListItem> UsersSelected { get; set; }
        public List<SelectListItem> CategoriesSelected { get; set; }

        public PostViewModel()
        {

        }

        public PostViewModel(Post post)
        {
            Id = post.Id;
            IdCategorie = post.IdCategorie;
            IdUser = post.IdUser;
            Name = post.Name;
            Slug = post.Slug;
            Content = post.Content;
            Created = post.Created;
            Comments = post.Comments;
            Category = post.Category;
            User = post.User;
        }

        public async Task InitListSeleted(BlogContext context)
        {
            CategoriesSelected = await context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToListAsync();
            UsersSelected = await context.Users.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.UserName }).ToListAsync();
        }
    }
}
