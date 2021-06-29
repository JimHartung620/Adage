using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Comment
    {
        [Key]
        public int CommentId {get;set;}

        [Required(ErrorMessage="Content is required!")]
        [MinLength(10, ErrorMessage="Comment must be at least 10 characters!")]
        [Display(Name="Comment: ")]
        public string Body {get;set;}

        // Many-One with Users who Commented
        public int CommenterId {get;set;}
        public User Commenter {get;set;}

        // Many-One with Comments on Post
        public int CommentedOnId {get;set;}
        public Post CommentedOn {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}