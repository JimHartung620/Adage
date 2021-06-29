using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Controllers
{
    public class LogRegController : Controller
    {
        private MyContext _context;
        public LogRegController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult LogRegIndex()
        {
            return View("LogRegIndex");
        }

        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return LogRegIndex();
                }
                User Admin = _context.Users.FirstOrDefault(u => u.isAdmin == true);
                if (Admin == null)
                {
                    newUser.isAdmin = true;
                }
                else
                {
                    newUser.isAdmin = false;
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return LogRegIndex();
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser returnUser)
        {
            if (ModelState.IsValid)
            {
                User existingUser = _context.Users.FirstOrDefault(u => u.Email == returnUser.LogEmail);
                if (existingUser == null)
                {
                    ModelState.AddModelError("LogEmail", "Email/Password is invalid!");
                    return LogRegIndex();
                }
                PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                var Result = Hasher.VerifyHashedPassword(returnUser, existingUser.Password, returnUser.LogPass);
                if (Result == 0)
                {
                    ModelState.AddModelError("LogEmail", "Email/Password is invalid!");
                    return LogRegIndex();
                }
                HttpContext.Session.SetInt32("UserId", existingUser.UserId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return LogRegIndex();
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogRegIndex");
        }
    }
}