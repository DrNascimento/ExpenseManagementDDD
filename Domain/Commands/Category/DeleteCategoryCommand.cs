using MediatR;

namespace Domain.Commands.Category;

public class DeleteCategoryCommand : CategoryCommand, IRequest<Unit>
{
    public DeleteCategoryCommand(Guid id) 
    {
        Id = id;
    }
}
