using FluentValidation;

namespace LibrarySystem.Contract.Category;

public class CategoryValidator : AbstractValidator<CategoryRequest>
{
    public CategoryValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name must be less than 50 characters");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(500)
            .WithMessage("Description must be less than 500 characters");
    }
}
