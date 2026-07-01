using ToDoApp.Db.Entities;

namespace ToDoApp.Common.Dtos;

public class ReturnUserDto
{
    public string Email { get; set; } = "";
    public List<TaskBaseDto> Tasks { get; set; } = new List<TaskBaseDto>();
}
