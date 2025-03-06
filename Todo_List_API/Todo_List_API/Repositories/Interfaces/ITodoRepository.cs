using Todo_List_API.DTOs;
using Todo_List_API.Entities;

namespace Todo_List_API.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoDTO> CreateAsync(CreateUpdateTodoDTO todo);
        Task<List<TodoDTO>> GetAllAsync(int userId, int page, int limit);
        Task<int> CountTodosByUser(int userId);
        Task<bool> DeleteTodo(int id, int userid);
        Task<TodoDTO> EditTodo(CreateUpdateTodoDTO todo);
    }
}
