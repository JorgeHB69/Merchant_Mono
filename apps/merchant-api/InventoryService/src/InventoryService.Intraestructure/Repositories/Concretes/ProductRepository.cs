using InventoryService.Intraestructure.Data;
using InventoryService.Intraestructure.Repositories.Bases;
using InventoryService.Domain.Concretes;
using Microsoft.EntityFrameworkCore;
using InventoryService.Intraestructure.Types;
using InventoryService.Intraestructure.Repositories.Interfaces;

namespace InventoryService.Intraestructure.Repositories.Concretes;

public class ProductRepository(InventoryDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    public override async Task<Product?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Where(e => e.IsActive)
            .AsSplitQuery()
            .Include(p => p.Images)
            .Include(p => p.Categories.Where(c => c.IsActive == true))
            .ThenInclude(c => c.ParentCategory)
            .Include(p => p.ProductVariants)
            .ThenInclude(pv => pv.Image)
            .Include(p => p.ProductVariants)
            .ThenInclude(pa => pa.Attributes)
            .ThenInclude(pa => pa.Variant)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await DbSet
            .Where(e => e.IsActive)
            .AsSplitQuery()
            .Include(p => p.Images)
            .Include(p => p.Categories.Where(c => c.IsActive == true))
            .ThenInclude(c => c.ParentCategory)
            .Include(p => p.ProductVariants)
            .ThenInclude(pv => pv.Image)
            .Include(p => p.ProductVariants)
            .ThenInclude(pa => pa.Attributes)
            .ThenInclude(pa => pa.Variant)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await DbSet.Where(e => e.IsActive)
            .AsSplitQuery()
            .Include(p => p.Images)
            .Include(p => p.Categories.Where(c => c.IsActive == true))
            .ThenInclude(c => c.ParentCategory)
            .Include(p => p.ProductVariants)
            .ThenInclude(pv => pv.Image)
            .Include(p => p.ProductVariants)
            .ThenInclude(pa => pa.Attributes)
            .ThenInclude(pa => pa.Variant)
            .OrderBy(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByStoreId(Guid storeId, int page, int pageSize, List<(string, SortingType)> sorting)
    {
        var query = DbSet
                        .Where(p => p.StoreId == storeId && p.IsActive == true)
                        .AsSplitQuery()
                        .Include(p => p.Images)
                        .Include(p => p.Categories.Where(c => c.IsActive == true))
                        .ThenInclude(c => c.ParentCategory)
                        .Include(p => p.ProductVariants)
                        .ThenInclude(pv => pv.Image)
                        .Include(p => p.ProductVariants)
                        .ThenInclude(pa => pa.Attributes)
                        .ThenInclude(pa => pa.Variant)
                        .AsQueryable();

        foreach (var (field, sortingType) in sorting)
        {
            if (sortingType != SortingType.NONE)
            {
                query = query.ApplySorting(field, sortingType);
            }
        }

        var pagedProducts = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return pagedProducts;
    }

}