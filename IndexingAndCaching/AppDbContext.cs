/// <summary>
/// Create a new database context class that inherits from
/// </summary>
public class AppDbContext : DbContext
{
    // Define the Products table and map it to the Product class
    public DbSet<Product> Products { get; set; }

    // Override the OnModelCreating method to configure the database schema
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Create an index on the Category and Price columns in the Products table
        modelBuilder.Entity<Product>()
            .HasIndex(p => new { p.Category, p.Price });
    }
}