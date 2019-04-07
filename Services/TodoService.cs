using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class TodoService : ITodoService
    {
        private DataContext _context;

        public TodoService(DataContext context)
        {
            _context = context;
        }

        public TodoItem GetById(int id)
        {
            return _context.TodoItems.Find(id);
        }

        public TodoItem Create(User user, TodoDto todoDto)
        {
            var todoItem = new TodoItem()
            {
                Title = todoDto.Title,
                Description = todoDto.Description,
                IsComplete = todoDto.IsComplete,
                User = user
            };
            // user.TodoItems.ToList().Add(todoItem);
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
            return todoItem;
        }

        public TodoItem Update(TodoItem todoParam)
        {
            var todo = _context.TodoItems.Find(todoParam.Id);

            if (todo != null)
            {
                todo.Title = todoParam.Title;
                todo.Description = todoParam.Description;
                todo.IsComplete = todoParam.IsComplete;

                _context.TodoItems.Update(todo);
                _context.SaveChanges();
            }

            return todo;
        }

        public TodoItem Delete(int id)
        {
            var todo = _context.TodoItems.Find(id);
            
            if (todo != null)
            {
                _context.TodoItems.Remove(todo);
                _context.SaveChanges();
            }

            return todo;
        }
    }
}