using LibrarySystem.Abstractions;
using LibrarySystem.Contract.Book;
using LibrarySystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController(IBookService book) : ControllerBase
{
    private readonly IBookService _book = book;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
    {
        var books = await _book.GetALlBooksAsync(cancellationToken);
        return books.IsSuccess ? Ok(books.Value) : books.ToProblem();
    }

    [HttpGet("Get/{Id}")]
    public async Task<IActionResult> GetBook([FromRoute]int Id,CancellationToken cancellationToken)
    {
        var book = await _book.GetBookAsync(Id,cancellationToken);
        return book.IsSuccess ? Ok(book.Value) : book.ToProblem();
    }


    [HttpPost("Create")]

    public async Task<IActionResult> CreateBook([FromBody] BookRequest request, CancellationToken cancellationToken)
    {
        var book = await _book.AddBooksAsync(request, cancellationToken);
        return book.IsSuccess ? CreatedAtAction(nameof(GetAllBooks), new { id = book.Value.Id }, book.Value) : book.ToProblem();
    }


    [HttpPut("Update/{Id}")]
    public async Task<IActionResult> UpdateBook([FromRoute] int Id, [FromBody] BookRequest request, CancellationToken cancellationToken)
    {
        var book = await _book.UpdateBookAsync(Id, request, cancellationToken);
        return book.IsSuccess ? NoContent() : book.ToProblem();

    }

    [HttpDelete("Delete/{Id}")]
    public async Task<IActionResult> DeleteBook([FromRoute] int Id, CancellationToken cancellationToken)
    {
        var book = await _book.DeleteBookAsync(Id, cancellationToken);
        return book.IsSuccess ? NoContent() : book.ToProblem();
    }


}
