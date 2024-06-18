using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingChallenge.Models
{
    public class Taskk
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
      
        public string Title { get; set; }

  
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }=DateTime.Now;

        public DateTime? CompletedDate { get; set; }

        // Foreign key
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        // Navigation property
        public User? user { get; set; }


        public Taskk()
        {

        }

        public Taskk(int taskId,string title,string description,string status,DateTime createdDate, DateTime completedDate, int userId, User user)
        {
            TaskId = taskId;
            Title = title;
            Description = description;
            Status = status;
            CreatedDate = createdDate;
            CompletedDate = completedDate;
            UserId = userId;
            this.user = user;
        }

       

    }

   
    
}

