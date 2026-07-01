namespace ToDoApp.Common.Dtos;

public class DeleteTaskDto
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
}
