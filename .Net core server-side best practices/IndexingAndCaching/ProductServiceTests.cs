[TestClass]
public class ProductServiceTests
{
    // This test verifies that the GetProductsAsync method returns cached data
    [TestMethod]
    public async Task GetProductsAsync_Should_Return_Cached_Data()
    {
        // Create an in-memory database context with test data
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "test_db")
            .Options;

        var dbContext = new AppDbContext(options);
        await dbContext.Products.AddRangeAsync(
            new Product { Name = "Book 1", Category = "Books", Price = 15M },
            new Product { Name = "Book 2", Category = "Books", Price = 25M },
            new Product { Name = "Book 3", Category = "Movies", Price = 15M },
            new Product { Name = "Book 4", Category = "Movies", Price = 25M });
        await dbContext.SaveChangesAsync();

        // Create an instance of the ProductService using a mock cache
        var cache = new MemoryCache(new MemoryCacheOptions());
        var productService = new ProductService(cache, dbContext);

        // Call the GetProductsAsync method twice with the same arguments
        const string category = "Books";
        const decimal minPrice = 10M;
        const decimal maxPrice = 20M;
        var result1 = await productService.GetProductsAsync(category, minPrice, maxPrice);
        var result2 = await productService.GetProductsAsync(category, minPrice, maxPrice);

        // Assert that the results are equal and that the second call returned cached data
        Assert.AreEqual(result1.Count(), result2.Count());
        Assert.AreEqual(result1.First().Name, result2.First().Name);

        // Assert that the results are not a List<Product> (as that is an implementation detail)
        Assert.IsFalse(result1 is List<Product>);
        Assert.IsTrue(result2 is List<Product>);
    }
}