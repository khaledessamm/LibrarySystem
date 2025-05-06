using FluentValidation;

namespace LibrarySystem.Contract.Book;

public class BookVaildator : AbstractValidator<BookRequest>
{
    public BookVaildator()
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

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal 0");

        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage("Author is required")
            .MaximumLength(50)
            .WithMessage("Author must be less than 50 characters");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock must be greater than or equal 0");
    }
}

