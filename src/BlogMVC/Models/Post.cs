using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Catégorie :")]
        public int IdCategorie { get; set; }

        [Required]
        [Display(Name = "Author :")]
        public int IdUser { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name :")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Slug :")]
        public string Slug { get; set; }

        [Required]
        [Display(Name = "Content :")]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public List<Comment> Comments { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }

        public Post()
        {
        }

        public Post(ViewModel.PostViewModel postVm)
        {
            Id = postVm.Id;
            IdCategorie = postVm.IdCategorie;
            IdUser = postVm.IdUser;
            Name = postVm.Name;
            Slug = postVm.Slug;
            Content = postVm.Content;
            Created = postVm.Created;
            Comments = postVm.Comments;
            Category = postVm.Category;
            User = postVm.User;
        }
    }
}