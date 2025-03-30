using merchant_api.Data.Data;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Bases;

namespace merchant_api.Data.Repositories.Concretes;

public class ImageRepository(PostgresContext context) : BaseRepository<Image>(context);