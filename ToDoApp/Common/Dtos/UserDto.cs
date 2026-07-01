using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Common.Dtos;

public class UserDto
{
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; } = "";
    
    [MaxLength(50)]
    public string Pass { get; set; } = "";
}
