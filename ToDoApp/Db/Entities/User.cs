namespace ToDoApp.Db.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = "";
    public string HashedPass { get; set; } = "";

    public List<TaskUser> Tasks { get; set; } = new List<TaskUser>();
}
