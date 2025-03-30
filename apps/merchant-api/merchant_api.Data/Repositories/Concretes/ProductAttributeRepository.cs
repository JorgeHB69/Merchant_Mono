using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes;

public class ProductAttributeRepository(PostgresContext context) : BaseRepository<ProductAttribute>(context)
{
    public override async Task<ProductAttribute> AddAsync(ProductAttribute entity)
    {
        var existingProductAttribute = await DbSet.FirstOrDefaultAsync(c => c.Value.ToLower() == entity.Value.ToLower() && c.IsActive);
        if (existingProductAttribute != null) return existingProductAttribute; 
        
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public override async Task<ProductAttribute?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Where(e => e.IsActive)
            .Include(c => c.Variant)          
            .Include(c => c.ProductVariant)         
            .FirstOrDefaultAsync(c => c.Id == id);  
    }

    public override async Task<IEnumerable<ProductAttribute>> GetAllAsync()
    {
        return await DbSet
            .Where(e => e.IsActive)
            .Include(c => c.Variant)          
            .Include(c => c.ProductVariant)        
            .OrderBy(c => c.CreatedAt) 
            .ToListAsync();
    }

    public override async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;
        
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
        return true;
    }
}