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
        public int IdPost { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public Post Post { get; set; }
    }
}
