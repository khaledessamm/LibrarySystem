namespace LibrarySystem.Contract.Book;

public record BookRequest
(
    string Name,
    string Description,
    decimal Price,
    string Author,
    int Stock,
    int CategoryId
);
