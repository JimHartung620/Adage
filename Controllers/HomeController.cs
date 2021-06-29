using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        private User LoggedUser
        {
            get {return _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));}
        }

//**************************************************** - Views - ****************************************************

        [HttpGet("dash")]
        public IActionResult Index()
        {
            IndexWrapper WMod = new IndexWrapper()
            {
                LoggedUserId = LoggedUser.UserId,
                AllPosts = _context.Posts
                    .Include(p => p.Poster)
                    .Include(p => p.Likes)
                    .Include(p => p.Comments)
                    .ThenInclude(c => c.Commenter)
                    .ToList()
            };

            return View("Index", WMod);
        }

        [HttpGet("users")]
        public IActionResult Users()
        {
            MembersWrapper WMod = new MembersWrapper()
            {
                UserLoggedIn = LoggedUser,
                AllUsers = _context.Users
                .OrderBy(u => u.CreatedAt)
                .ToList()
            };

            return View("Users", WMod);
        }

        [HttpGet("profile")]
        public IActionResult Profile()
        {
            ProfileWrapper WMod = new ProfileWrapper()
            {
                UserLoggedIn = LoggedUser,
                UserPosts = _context.Posts
                    .Include(p => p.Comments)
                    .ThenInclude(c => c.Commenter)
                    .Where(p => p.PosterId == LoggedUser.UserId)
                    .ToList()
            };
            //User UserLoggedIn = LoggedUser.Include(u => u.Posts).ToList();
            return View("Profile", WMod);
        }

        [HttpGet("profile/{UserId}/edit")]
        public IActionResult EditProfile(int UserID)
        {
            User ToEdit = _context.Users.FirstOrDefault(u => u.UserId ==UserID);
            return View("Edit", ToEdit);
        }

//**************************************************** - Posts - ****************************************************

        [HttpPost("/create/post")]
        public IActionResult CreatePost(Post newPost)
        {
            if (ModelState.IsValid)
            {
                newPost.PosterId = LoggedUser.UserId;
                _context.Add(newPost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Index();
            }
        }

        [HttpPost("/create/comment/{PostId}")]
        public IActionResult CreateComment(Comment newComment, int postID)
        {
            if (ModelState.IsValid)
            {
                newComment.CommenterId = LoggedUser.UserId;
                newComment.CommentedOnId = postID;
                _context.Add(newComment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Index();
            }
        }

//**************************************************** - Users - ****************************************************

        [HttpGet("Delete/{userId}")]
        public IActionResult Delete(int userID)
        {
            User UserToDelete = _context.Users.SingleOrDefault(u => u.UserId == userID);
            /*if (UserToDelete == LoggedUser)
            {
                string alert = "You cannot delete your account!";
                string caption = "Alert!";
                MessageBoxButtons
            }*/
            _context.Users.Remove(UserToDelete);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }

        
        [HttpPost("profile/{UserId}/edit")]
        //public IActionResult UpdateProfile(int userID, User UserToEdit)
        public IActionResult UpdateProfile(int userID)
        {
            User UserToEdit = _context.Users.SingleOrDefault(u => u.UserId == userID);
            //UserToEdit.UserId = userID;
            //UserToEdit.Password = BeingEdited.Password;
            //UserToEdit.UserId = Model.UserId;
            //UserToEdit.Description = Model.Description;
            _context.Update(UserToEdit);
            _context.Entry(UserToEdit).Property("CreatedAt").IsModified = false;
            _context.SaveChanges();
            return RedirectToAction("Index");
            /*
            if (ModelState.IsValid)
            {
                /*
                if (_context.Users.Any(u => u.Email == UserToEdit.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return EditProfile();
                }
                
                UserToEdit.UserId = userID;
                _context.Update(UserToEdit);
                _context.Entry(UserToEdit).Property("CreatedAt").IsModified = false;
                /*
                _context.Entry(UserToEdit).Property("Password").IsModified = false;
                _context.Entry(UserToEdit).Property("ConfirmPassword").IsModified = false;
                _context.Entry(UserToEdit).Property("isAdmin").IsModified = false;
                _context.Entry(UserToEdit).Property("Posts").IsModified = false;
                _context.Entry(UserToEdit).Property("Comments").IsModified = false;
                _context.Entry(UserToEdit).Property("Likes").IsModified = false;
                _context.Entry(UserToEdit).Property("UserId").IsModified = false;
                
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                //return EditProfile(userID);
                return RedirectToAction("Index");
            }
            */
        }
    }
}