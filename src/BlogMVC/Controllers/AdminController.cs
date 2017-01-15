using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogMVC.Data;
using BlogMVC.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.Http.Authentication;

namespace BlogMVC.Controllers
{
    public class AdminController : BaseController
    {
        private readonly BlogContext _context;

        public AdminController(BlogContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("/Admin/")]
        public IActionResult Index()
        {
            return View(new User());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            CheckUser(user);

            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }

            var identity = new ClaimsIdentity("MyCookieMiddlewareInstance");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal, new AuthenticationProperties());

            return RedirectToAction("Index", "Posts", new { area = "Admin"});
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            return RedirectToAction("Index", "Posts");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private void CheckUser(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.UserName)) return;

            var passwordCrypt = GetSha1(user.Password);
            var userDb = _context.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == passwordCrypt);
            if (userDb == null) ModelState.AddModelError("", "Bad credentials !");
        }

        private string GetSha1(string input)
        {
            var hash = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

    }
}
