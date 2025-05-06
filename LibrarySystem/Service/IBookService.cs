using LibrarySystem.Abstractions;
using LibrarySystem.Contract.Book;

namespace LibrarySystem.Service;

public interface IBookService
{
    Task<Result<IEnumerable<BookResponse>>> GetALlBooksAsync(CancellationToken cancellationToken);
    Task<Result<BookResponse>> AddBooksAsync(BookRequest request,CancellationToken cancellationToken);
    Task<Result<BookResponse>> GetBookAsync(int Id,CancellationToken cancellationToken);
    Task<Result<BookResponse>> UpdateBookAsync(int Id, BookRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteBookAsync(int Id, CancellationToken cancellationToken);
}
