using System.Text.Json.Serialization;

namespace Todo_List_API.DTOs
{
    public class CreateUpdateTodoDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
