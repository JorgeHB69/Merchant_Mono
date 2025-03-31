using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Repositories.Concretes.Inventory;

public class ProductVariantRepository(PostgresContext context) : BaseRepository<ProductVariant>(context)
{
    public override async Task<ProductVariant?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Where(e => e.IsActive)
            .AsSplitQuery()
            .Include(c => c.Attributes)
            .ThenInclude(pv => pv.Variant)
            .Include(c => c.Image)
            .FirstOrDefaultAsync(c => c.Id == id);  
    }

    public override async Task<IEnumerable<ProductVariant>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await DbSet.Where(e => e.IsActive)
            .AsSplitQuery()
            .Include(c => c.Attributes)
            .ThenInclude(pv => pv.Variant)
            .Include(c => c.Image)
            .OrderBy(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public override async Task<IEnumerable<ProductVariant>> GetAllAsync()
    {
        return await DbSet
            .Where(e => e.IsActive)
            .AsSplitQuery()
            .Include(c => c.Attributes)
            .Include(c => c.Image)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }
}