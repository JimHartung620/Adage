using System;
using System.Collections.Generic;

namespace Website.Models
{
    public class IndexWrapper
    {
        public int LoggedUserId {get;set;}

        public Post NewPost {get;set;}

        public Comment NewComment {get;set;}
        
        public List<Post> AllPosts {get;set;}
    }

    public class ProfileWrapper
    {
        public User UserLoggedIn {get;set;}

        public List<Post> UserPosts {get;set;}
    }

    
    public class MembersWrapper
    {
        public User UserLoggedIn {get;set;}
        public List<User> AllUsers {get;set;}
    }
    
}