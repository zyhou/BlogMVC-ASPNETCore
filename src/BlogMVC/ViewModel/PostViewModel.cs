using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    }
}
