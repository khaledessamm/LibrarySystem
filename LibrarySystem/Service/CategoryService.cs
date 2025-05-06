using LibrarySystem.Abstractions;
using LibrarySystem.Contract.Book;
using LibrarySystem.Contract.Category;
using LibrarySystem.Data;
using LibrarySystem.Errors;
using LibrarySystem.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Service;

public class CategoryService(ApplicationDbContext context) : ICategoryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<CategoryResponse>> AddCategoryAsync(CategoryRequest request, CancellationToken cancellationToken)
    {
    

        var category = request.Adapt<Category>();

        await _context.Categories.AddAsync(category, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        var response = category.Adapt<CategoryResponse>();
        return Result.Success(response);
    }

    public async Task<Result> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var isexsist = await _context.Categories.FindAsync(id);
        if (isexsist is null)

            return Result.Failure<CategoryResponse>(CategoryError.NotFound);
        _context.Remove(isexsist);
        _context.SaveChanges(); //since using setnull we have to remove category id from book table first
        return Result.Success();
    }

    public async Task<Result<IEnumerable<CategoryResponse>>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.Include(c => c.Books)
            .Select(c => new CategoryResponse
            (
               c.Id,
                c.Name,
                c.Description,
                c.Books.Select(b => new BookResponse
                (
                     b.Id,
                     b.Name,
                     b.Description,
                     b.Price,
                     b.Author,
                     b.Stock,
                     b.Category.Id,
                     b.Category.Name

            )))).AsNoTracking().ToListAsync(cancellationToken);

   
        var response=categories.Adapt<IEnumerable<CategoryResponse>>();


        return Result.Success(response);


    }

    public async Task<Result<CategoryResponse>> GetCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var isexsist =await  _context.Categories.FindAsync(id, cancellationToken);
        if (isexsist == null)
        {
            return (Result.Failure<CategoryResponse>(CategoryError.NotFound));
        }
        var includingbooks=await _context.Categories.Include(c => c.Books)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return Result.Success(includingbooks.Adapt<CategoryResponse>());
    }

    public async Task<Result<CategoryResponse>> UpdateCategoryAsync(int id, CategoryRequest request, CancellationToken cancellationToken)
    {
        var isexsist = await _context.Categories.FindAsync(id, cancellationToken);
        if (isexsist == null)
        
            return (Result.Failure<CategoryResponse>(CategoryError.NotFound));

       isexsist.Name = request.Name;
        isexsist.Description = request.Description;
        _context.Update(isexsist);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(isexsist.Adapt<CategoryResponse>());


    }
}
