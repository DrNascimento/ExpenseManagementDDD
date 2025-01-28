using MediatR;

namespace Domain.Commands.Category;

public class UpdateCategoryCommand : CategoryCommand, IRequest<Unit>
{
    public UpdateCategoryCommand(Guid id, string name) 
    {
        Id = id;
        Name = name;
    }
}
