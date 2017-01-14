using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Controllers
{
    public class BaseController :  Controller
    {
        public const int ITEM_PER_PAGE = 2;

    }
}
