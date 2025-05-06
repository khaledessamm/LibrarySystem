using LibrarySystem.Abstractions;
using LibrarySystem.Contract.Book;
using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Errors;
using Mapster;
using LibrarySystem.Models;

namespace LibrarySystem.Service;

public class BookService(ApplicationDbContext context) : IBookService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<BookResponse>> AddBooksAsync(BookRequest request, CancellationToken cancellationToken)
    {
       
        var book = request.Adapt<Book>();
        await _context.Books.AddAsync(book, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var categoryname=await _context.Books.Include(x => x.Category)
           .FirstOrDefaultAsync(b=>b.Id==book.Id,cancellationToken);
         var response =categoryname.Adapt<BookResponse>();
        return Result.Success(response);
    }

    public async Task<Result> DeleteBookAsync(int Id, CancellationToken cancellationToken)
    {
        var isexsist = await _context.Books.FindAsync(Id , cancellationToken);
        if (isexsist is null )
        
            return  Result.Failure<BookResponse>(BookError.NotFound);
        
        _context.Remove(isexsist);
           await _context.SaveChangesAsync(cancellationToken);
           return Result.Success();
    }

    public async Task<Result<IEnumerable<BookResponse>>> GetALlBooksAsync(CancellationToken cancellationToken)
    {
        var books=await _context.Books.Include(x => x.Category)
            .Select(x => new BookResponse
            (
                x.Id,
                x.Name,
                x.Author,
                x.Price,
                x.Description,
                x.Stock,
           x.CategoryId??0,
                 x.Category!= null ? x.Category.Name : "not categorized yet"
                 )
            ).
            AsNoTracking().ToListAsync(cancellationToken);

      return Result.Success(books.Adapt<IEnumerable<BookResponse>>());
    }

    public async Task<Result<BookResponse>> GetBookAsync(int Id, CancellationToken cancellationToken)
    {
       var isexsist=await _context.Books.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        if (isexsist is null)

            Result.Failure<BookResponse>(BookError.NotFound);

             var categoryname = await _context.Books.Include(x => x.Category)
             .FirstOrDefaultAsync(b => b.Id == Id, cancellationToken);
             var response = categoryname.Adapt<BookResponse>();

                return Result.Success(response); ;
    }

    public async Task<Result<BookResponse>> UpdateBookAsync(int Id, BookRequest request, CancellationToken cancellationToken)
    {
        var isexsist = await _context.Books.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        if(isexsist is null)
        {
            return Result.Failure<BookResponse>(BookError.NotFound);
        }
      
        isexsist.Name = request.Name;
        isexsist.Author = request.Author;
        isexsist.Price = request.Price;
        isexsist.Description = request.Description;
        isexsist.Stock = request.Stock;
        isexsist.CategoryId = request.CategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        var categoryname = await _context.Books.Include(x => x.Category)
        .FirstOrDefaultAsync(b => b.Id == Id, cancellationToken);
        var response = categoryname.Adapt<BookResponse>();
        return Result.Success(response);
       
    }
}

