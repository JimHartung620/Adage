using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Like
    {
        [Key]
        public int LikeId {get;set;}

        public bool Disliked {get;set;}

        // Many-One with Users who Like
        public int LikedById {get;set;}
        public User LikedBy {get;set;}

        // Many-One with Likes on Post
        public int PostLikedId {get;set;}
        public Post PostLiked {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}