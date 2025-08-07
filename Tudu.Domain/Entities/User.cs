using System.ComponentModel.DataAnnotations;

namespace Tudu.Domain.Entities
    {
    public class User
        {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
        }
    }
