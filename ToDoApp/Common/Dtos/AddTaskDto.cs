using ToDoApp.Enums;

namespace ToDoApp.Common.Dtos;

public class AddTaskDto : TaskBaseDto
{
    public Guid UserId { get; set; }
    public Category Category { get; set; }
}
