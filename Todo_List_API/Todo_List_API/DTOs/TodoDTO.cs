using System.Text.Json.Serialization;

namespace Todo_List_API.DTOs
{
    public class TodoDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; } =  0;
        public string? Title { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
