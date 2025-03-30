using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes;

public class VariantRepository(PostgresContext context) : BaseRepository<Variant>(context)
{
    public override async Task<Variant> AddAsync(Variant entity)
    {
        var existingVariant = await DbSet.FirstOrDefaultAsync(v => v.Name.ToLower() == entity.Name.ToLower() && v.IsActive);
        if (existingVariant != null) return existingVariant; 
        
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    
    public override async Task<Variant?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Where(e => e.IsActive)
            .Include(c => c.ProductAttributes)
            .ThenInclude(pa => pa.ProductVariant)
            .FirstOrDefaultAsync(c => c.Id == id);  
    }

    public override async Task<IEnumerable<Variant>> GetAllAsync()
    {
        return await DbSet
            .Where(e => e.IsActive)
            .Include(c => c.ProductAttributes)          
            .ToListAsync();
    }
}