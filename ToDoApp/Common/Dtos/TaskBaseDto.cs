using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Common.Dtos;

public class TaskBaseDto
{
    [MaxLength(30)]
    public string Name { get; set; } = "";
    [MaxLength(100)]
    public string Description { get; set; } = "";
}
