using System.ComponentModel.DataAnnotations;

namespace CodingChallenge.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
     
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
       
        public string Email { get; set; }

     

        public User()
        {

        }

        public User(int userId, string userName, string password, string email)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Email = email;
          
        }
    }
}
