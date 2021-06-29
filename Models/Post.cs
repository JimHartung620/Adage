using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Post
    {
        [Key]
        public int PostId {get;set;}

        [Required(ErrorMessage="Title is required!")]
        [MinLength(2, ErrorMessage="Title must be at least 2 characters!")]
        [Display(Name="Title: ")]
        public string Title {get;set;}

        [Required(ErrorMessage="Content is required!")]
        [MinLength(20, ErrorMessage="Post must be at least 20 characters!")]
        [Display(Name="Post: ")]
        public string Body {get;set;}

        // Many-One with User who Posted
        public int PosterId {get;set;}
        public User Poster {get;set;}

        // One-Many with Comments on Post
        public List<Comment> Comments {get;set;}

        // Many-Many with Users who Like Posts
        public List<Like> Likes {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}