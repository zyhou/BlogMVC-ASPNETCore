using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [HiddenInput]
        public int IdPost { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "username")]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Display(Name = "email")]
        public string Mail { get; set; }

        [Required]
        [Display(Name = "comment")]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public Post Post { get; set; }
    }
}
