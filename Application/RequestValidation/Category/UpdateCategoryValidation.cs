using Application.DTO.Input.Category;

namespace Application.RequestValidation.Category;

public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryInput>
{
    public UpdateCategoryValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
