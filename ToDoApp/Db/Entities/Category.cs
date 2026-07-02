namespace ToDoApp.Db.Entities;

public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";


    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

}
