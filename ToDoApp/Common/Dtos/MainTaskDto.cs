
namespace ToDoApp.Common.Dtos;

public class MainTaskDto : TaskBaseDto
{
    public Guid Id { get; set; }
    public string Category { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
}
