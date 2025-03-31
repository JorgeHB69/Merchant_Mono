using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Inventory;

public class CategoryRepository(PostgresContext context) : BaseRepository<Category>(context)
{
    public override async Task<Category> AddAsync(Category entity)
    {
        var existingCategory = await DbSet.FirstOrDefaultAsync(c => c.Name == entity.Name && c.IsActive);
        if (existingCategory != null) return existingCategory; 
        
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public override async Task<Category?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Where(e => e.IsActive)
            .Include(c => c.ParentCategory)          
            .Include(c => c.SubCategories)
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);  
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await DbSet
            .Where(e => e.IsActive)
            .Include(c => c.ParentCategory)       
            .Include(c => c.SubCategories)          
            .OrderBy(c => c.CreatedAt) 
            .ToListAsync();
    }
}