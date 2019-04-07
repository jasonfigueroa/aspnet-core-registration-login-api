using WebApi.Entities;

namespace WebApi.Dtos
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public int UserId { get; set; }
    }
}