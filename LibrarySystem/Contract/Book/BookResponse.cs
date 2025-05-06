namespace LibrarySystem.Contract.Book;

public record BookResponse
(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Author,
    int Stock,

    int CategoryId,
    string CategoryName

);
