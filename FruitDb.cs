using Microsoft.EntityFrameworkCore;

class FruitDb : DbContext
{
    public FruitDb(DbContextOptions<FruitDb> options)
        : base(options) { }

    public DbSet<Fruit> Fruits => Set<Fruit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Fruit>()
            .HasData(
            new Fruit { Id = 1, Name = "Apple", Instock = true, Price = 15},
            new Fruit { Id = 2, Name = "Banana", Instock = false, Price = 5},
            new Fruit { Id = 3, Name = "Orange", Instock = true, Price=4 }
            );
    }
}