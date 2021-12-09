using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using loginRegDec.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace loginRegDec.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]

        public IActionResult Register(User newUser)
        {


            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.email == newUser.email))
                {
                    ModelState.AddModelError("email", "Email is already in database");
                    return View("Index");

                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                _context.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("success")]

        public IActionResult Success()
        {
            return View("Success");
        }

        [HttpPost("login")]
        public IActionResult Login (LogUser loguser)
        {
            if(ModelState.IsValid)
            {
                User userinDb = _context.Users.FirstOrDefault(u => u.email == loguser.lemail);
                // when we search using first or default if nothing comes back we get null, if something is found we get back a user object
                if(userinDb == null)
                {
                    ModelState.AddModelError("lemail", "Invalid Login Attempt");
                    return View("Index");
                }
                PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
                PasswordVerificationResult result = Hasher.VerifyHashedPassword(loguser, userinDb.password, loguser.lpassword);
                // when the verification runs, it will pass back a 1 or a 0 - 1 means successful and 0 means unsuccessful
                if(result == 0)
                {
                    ModelState.AddModelError("lemail", "Invalid Login Attempt");
                    return View("Index");
                }
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
