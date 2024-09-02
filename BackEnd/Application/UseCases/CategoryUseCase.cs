using Application.DTOs;
using Application.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class CategoryUseCase
{
    private readonly ICrudDefault<Category, CategoryDto> _categoryAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryUseCase"/> class.
    /// </summary>
    /// <param name="categoryAdapter">The category adapter.</param>
    public CategoryUseCase(ICrudDefault<Category, CategoryDto> categoryAdapter)
    {
        this._categoryAdapter = categoryAdapter;
    }
    
    /// <summary>
    /// Creates a new category based on the provided <paramref name="categoryDto"/>.
    /// </summary>
    /// <param name="categoryDto">The data transfer object containing the category information to create.</param>
    /// <returns>The newly created <see cref="Category"/> entity.</returns>
    public async Task<Category> Create(CategoryDto categoryDto)
    {
        var user = new Category
        {
            Name = categoryDto.Name,
            UserId = categoryDto.UserId
        };
    
        return await this._categoryAdapter.Create(user);
    }

    /// <summary>
    /// Retrieves a list of <see cref="Category"/> entities based on the provided parameters.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <param name="category">The optional category ID to filter the results.</param>
    /// <returns>A list of <see cref="Category"/> entities matching the provided parameters.</returns>
    public async Task<List<Category>> Get(int id, int? category)
    {
        return await this._categoryAdapter.GetAll(id, 0);

    }
}