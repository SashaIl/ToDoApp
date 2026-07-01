using ToDoApp.Enums;

namespace ToDoApp.Common.Dtos;

public class UpdateTaskDto : TaskBaseDto
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public Category Category { get; set; }
}
