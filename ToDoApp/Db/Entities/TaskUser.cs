
namespace ToDoApp.Db.Entities;

public class TaskUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedDate { get; set; } = DateTime.Now;


    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = new Category();

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
