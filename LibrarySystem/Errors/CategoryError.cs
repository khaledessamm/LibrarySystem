using LibrarySystem.Abstractions;

namespace LibrarySystem.Errors;

public class CategoryError
{
    public static readonly Error NotFound = new("Category.Notfound", "nothing was found with given id", StatusCodes.Status404NotFound);
    public static readonly Error NotAdded = new("Category.NotAdded", "nothing was Added try again ", StatusCodes.Status400BadRequest);
}
