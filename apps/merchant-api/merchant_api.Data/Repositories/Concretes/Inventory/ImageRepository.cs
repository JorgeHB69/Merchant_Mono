using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Bases;

namespace merchant_api.Data.Repositories.Concretes.Inventory;

public class ImageRepository(PostgresContext context) : BaseRepository<Image>(context);