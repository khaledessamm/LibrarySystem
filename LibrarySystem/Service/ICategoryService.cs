using LibrarySystem.Abstractions;
using LibrarySystem.Contract.Category;

namespace LibrarySystem.Service;

public interface ICategoryService
{
    Task<Result<IEnumerable<CategoryResponse>>> GetCategoriesAsync(CancellationToken cancellationToken);

    Task<Result<CategoryResponse>> GetCategoryAsync(int id, CancellationToken cancellationToken);

    Task<Result<CategoryResponse>> AddCategoryAsync(CategoryRequest request, CancellationToken cancellationToken);

    Task<Result<CategoryResponse>> UpdateCategoryAsync(int id, CategoryRequest request, CancellationToken cancellationToken);

    Task<Result> DeleteCategoryAsync(int id, CancellationToken cancellationToken);
}
