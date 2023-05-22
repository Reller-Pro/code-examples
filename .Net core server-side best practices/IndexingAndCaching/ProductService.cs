/// <summary>
/// This class represents a service for retrieving products from a database
/// </summary>
public class ProductService : IProductService
{
    private readonly IMemoryCache _cache;

    // Constructor injection of IMemoryCache
    public ProductService(IMemoryCache cache)
    {
        _cache = cache;
    }

    // This method retrieves products that belong to a certain category, 
    // are within a certain price range, and optionally filters by keyword.
    // It first tries to retrieve the products from the cache, and if they are not present,
    // it queries the database and stores the results in cache before returning them.
    public async Task<IEnumerable<Product>> GetProductsAsync(string category, decimal minPrice, decimal maxPrice, string keyword = null)
    {
        // Generate a unique cache key for this query
        var key = $"products_{category}_{minPrice}_{maxPrice}_{keyword}";

        // Try to retrieve the products from cache
        if (_cache.TryGetValue(key, out IEnumerable<Product> products))
        {
            return products;
        }

        // If products are not in cache, query the database
        IQueryable<Product> query = _dbContext.Products
            .Where(p => p.Category == category && p.Price >= minPrice && p.Price <= maxPrice);

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(p => p.Name.Contains(keyword));
        }

        products = await query.ToListAsync();

        // Store the products in cache with a sliding expiration of 5 minutes
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        _cache.Set(key, products, cacheOptions);

        return products;
    }
}