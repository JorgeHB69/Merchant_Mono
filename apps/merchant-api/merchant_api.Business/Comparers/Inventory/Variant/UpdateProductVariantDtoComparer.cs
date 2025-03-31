using merchant_api.Business.Dtos.Inventory.ProductVariants;

namespace merchant_api.Business.Comparers.Inventory.Variant;

public class UpdateProductVariantDtoComparer : IEqualityComparer<UpdateProductVariantDto>
{
    public bool Equals(UpdateProductVariantDto? x, UpdateProductVariantDto? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id.Equals(y.Id);
    }

    public int GetHashCode(UpdateProductVariantDto obj)
    {
        return obj.Id.GetHashCode();
    }
}