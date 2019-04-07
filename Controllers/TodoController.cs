using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private ITodoService _todoService;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TodoController(
            ITodoService todoService,
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _todoService = todoService;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // currently returns item created but without the id
        [HttpPost]
        public IActionResult PostTodoItem([FromBody]TodoDto todoDto)
        {
            var user =  _userService.GetById(todoDto.UserId);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid user id" });
            }

            try 
            {
                // save 
                _todoService.Create(user, todoDto);
                return Created("localhost:4000", todoDto);
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // [HttpGet]
        // public IActionResult GetAll(int id)
        // {
        //     var todoItems =  _todoService.GetAll(id);
        //     var todoDtos = _mapper.Map<IList<TodoDto>>(todoItems);
        //     return Ok(todoDtos);
        // }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo =  _todoService.GetById(id);
            
            if (todo == null)
            {
                return NotFound(new { message = "invalid todo id" });
            }

            var todoDto = _mapper.Map<TodoDto>(todo);
            return Ok(todoDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]TodoDto todoDto)
        {
            var todo = _mapper.Map<TodoItem>(todoDto);
            todo.Id = id;

            var createdTodo = _todoService.Update(todo);
            
            if (createdTodo == null)
            {
                return NotFound(new { message = "todo item not found"});
            }

            var createdTodoDto = _mapper.Map<TodoDto>(createdTodo);    
            return Ok(createdTodoDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _todoService.Delete(id);

            if (todo == null)
            {
                return NotFound(new { message = "invalid todo id" });
            }

            var todoDto = _mapper.Map<TodoDto>(todo);
            return Ok(todoDto);
        }
    }
}
