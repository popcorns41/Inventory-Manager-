using InventoryManager.Application.Categories;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]

    public async Task<ActionResult<IReadOnlyCollection<CategoryResponse>>> GetCategories(
        CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetCategoriesAsync(cancellationToken);

        return Ok(categories);
    }

    [HttpGet("{id:int}")]

    public async Task<ActionResult<IReadOnlyCollection<CategoryResponse>>> GetCategoriesById(
        int id,
        CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(
            id,
            cancellationToken
        );

        if (category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> CreateCategory(
        CreateCategoryRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var category = await _categoryService.CreateCategoryAsync(
                request,
                cancellationToken
            );

            return CreatedAtAction(
                nameof(GetCategoriesById),
                new {id = category.Id},
                category
            );
        }catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]

    public async Task<IActionResult> DeleteCategory(
        int id,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var deleted = await _categoryService.DeleteCategoryAsync(
                id,
                cancellationToken
            );

            if (!deleted) return NotFound();

            return NoContent();   
        } catch (InvalidOperationException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult<CategoryResponse>> UpdateCategory(
        int id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(
                id,
                request,
                cancellationToken
            );

            if (updatedCategory is null) return NotFound();

            return Ok(updatedCategory);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }
}