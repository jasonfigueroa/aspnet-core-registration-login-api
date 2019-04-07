using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface ITodoService
    {
        TodoItem Create(User user, TodoDto todoDto);
        TodoItem GetById(int id);
        TodoItem Update(TodoItem todoParam);
        TodoItem Delete(int id);
    }
}