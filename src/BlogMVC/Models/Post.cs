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
        public int IdCategorie { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public List<Comment> Comments { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
    }
}