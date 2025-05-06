using LibrarySystem.Contract.Book;

namespace LibrarySystem.Contract.Category;

public record CategoryResponse
(
  int Id,
    string Name,
    string Description,
    IEnumerable<BookResponse> Books

);