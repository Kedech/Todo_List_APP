using Todo_List_API.DTOs;
using Todo_List_API.Entities;

namespace Todo_List_API.Services.Interfaces
{
    public interface ITodoService
    {
        Task<ListTodoDTO<TodoDTO>> ListTodo(int userId, int page, int limit);
        Task<TodoDTO> CreateTodo(CreateUpdateTodoDTO todoDTO);
        Task<bool> DeleteTodo(int id, int userid);
        Task<TodoDTO> EditTodo(CreateUpdateTodoDTO todoDTO);
    }
}
