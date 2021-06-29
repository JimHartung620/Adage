using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required(ErrorMessage="Username is required!")]
        [MinLength(3, ErrorMessage="Username must be at least 3 characters!")]
        [Display(Name="Username: ")]
        public string UserName {get;set;}

        [Required(ErrorMessage="First name is required!")]
        [MinLength(2, ErrorMessage="First name must be at least 2 characters!")]
        [Display(Name="First Name: ")]
        public string FirstName {get;set;}

        [Required(ErrorMessage="Last name is required!")]
        [MinLength(2, ErrorMessage="Last name must be at least 2 characters!")]
        [Display(Name="Last Name: ")]
        public string LastName {get;set;}

        [Required(ErrorMessage="Email address is required!")]
        [EmailAddress(ErrorMessage="Must be valid email address!")]
        [Display(Name="Email: ")]
        public string Email {get;set;}

        [Required(ErrorMessage="Password is required!")]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters!")]
        [DataType(DataType.Password)]
        [Display(Name="Password: ")]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password: ")]
        public string ConfirmPassword {get;set;}

        [MinLength(10, ErrorMessage="Description must be at least 10 characters!")]
        [Display(Name="Description: ")]
        public string Description {get;set;}

        public bool isAdmin {get;set;}

        // One-Many with Posts made by User
        public List<Post> Posts {get;set;}

        // One-Many with Comments made by Users
        public List<Comment> Comments {get;set;}

        // Many-Many with Posts Liked by Users
        public List<Like> Likes {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }

    public class LoginUser
    {
        [Required(ErrorMessage="You must enter an email address!")]
        [Display(Name="Email: ")]
        public string LogEmail {get;set;}

        [Required(ErrorMessage="You must enter a password!")]
        [DataType(DataType.Password)]
        [Display(Name="Password: ")]
        public string LogPass {get;set;}
    }
}