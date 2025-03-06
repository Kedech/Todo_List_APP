namespace Todo_List_API.DTOs
{
    public class ListTodoDTO<T>
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T>? Data { get; set; }
    }
}
