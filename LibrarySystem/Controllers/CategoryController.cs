using LibrarySystem.Abstractions;
using LibrarySystem.Contract.Category;
using LibrarySystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService category) : ControllerBase
{
    private readonly ICategoryService _category = category;



    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _category.GetCategoriesAsync(cancellationToken);
        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _category.AddCategoryAsync(request, cancellationToken);
        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem(); 
        
       
    }

    [HttpGet("Get/{Id}")]
    public async Task<IActionResult> Get([FromRoute] int Id, CancellationToken cancellationToken)
    {
        var result = await _category.GetCategoryAsync(Id, cancellationToken);
        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }

    [HttpPut("Update/{Id}")]
    public async Task<IActionResult> Update([FromRoute]int Id, [FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _category.UpdateCategoryAsync(Id, request, cancellationToken);
        return result.IsSuccess ?
             NoContent() :
             result.ToProblem();
    }

    [HttpDelete("Delete/{Id}")]
    public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken cancellationToken)
    { 
    var result=await _category.DeleteCategoryAsync(Id, cancellationToken);
        return result.IsSuccess ?
            NoContent() :
            result.ToProblem();

    }
}
