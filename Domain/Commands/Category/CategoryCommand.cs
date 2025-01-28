namespace Domain.Commands.Category;

public class CategoryCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? UserId { get; set; }
}
