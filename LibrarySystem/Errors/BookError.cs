using LibrarySystem.Abstractions;

namespace LibrarySystem.Errors;

public class BookError
{
    public static readonly Error NotFound = new("Book.Notfound", "nothing was found with given id", StatusCodes.Status404NotFound);
    public static readonly Error NotAdded = new("Book.NotAdded", "nothing was Added try again ", StatusCodes.Status400BadRequest);

}

