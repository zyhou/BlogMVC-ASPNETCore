using BlogMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Models.Paging
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize
        {
            get
            {
                return (int)Math.Ceiling((PageCount / (decimal)BaseController.ITEM_PER_PAGE));
            }
        }
    }
}
