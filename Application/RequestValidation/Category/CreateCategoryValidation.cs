using Application.DTO.Input.Category;

namespace Application.RequestValidation.Category;

public class CreateCategoryValidation : AbstractValidator<CreateCategoryInput>
{
    public CreateCategoryValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
