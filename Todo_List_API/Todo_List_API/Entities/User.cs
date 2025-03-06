using System.ComponentModel.DataAnnotations;

namespace Todo_List_API.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Salt { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Todo>? Todos { get; set; }
    }
}
