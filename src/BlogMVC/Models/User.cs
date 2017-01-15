using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        public List<Post> Posts { get; set; }
    }
}
