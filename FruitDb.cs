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
            new Fruit { Id = 3, Name = "Orange", Instock = true, Price=4 },
            new Fruit { Id = 4, Name = "Mango", Instock = true, Price = 12 },
            new Fruit { Id = 5, Name = "Pineapple", Instock = false, Price = 20 },
            new Fruit { Id = 6, Name = "Strawberry", Instock = true, Price = 8 },
            new Fruit { Id = 7, Name = "Blueberry", Instock = true, Price = 10 },
            new Fruit { Id = 8, Name = "Grapes", Instock = false, Price = 6 },
            new Fruit { Id = 9, Name = "Watermelon", Instock = true, Price = 18 },
            new Fruit { Id = 10, Name = "Papaya", Instock = false, Price = 14 },
            new Fruit { Id = 11, Name = "Kiwi", Instock = true, Price = 7 },
            new Fruit { Id = 12, Name = "Peach", Instock = true, Price = 11 },
            new Fruit { Id = 13, Name = "Plum", Instock = false, Price = 5 },
            new Fruit { Id = 14, Name = "Pomegranate", Instock = true, Price = 9 },
            new Fruit { Id = 15, Name = "Cherry", Instock = true, Price = 15 },
            new Fruit { Id = 16, Name = "Apricot", Instock = false, Price = 13 },
            new Fruit { Id = 17, Name = "Lychee", Instock = true, Price = 12 },
            new Fruit { Id = 18, Name = "Guava", Instock = false, Price = 8 },
            new Fruit { Id = 19, Name = "Blackberry", Instock = true, Price = 10 },
            new Fruit { Id = 20, Name = "Raspberry", Instock = true, Price = 9 },
            new Fruit { Id = 21, Name = "Fig", Instock = false, Price = 11 },
            new Fruit { Id = 22, Name = "Coconut", Instock = true, Price = 14 },
            new Fruit { Id = 23, Name = "Cantaloupe", Instock = false, Price = 7 },
            new Fruit { Id = 24, Name = "Cranberry", Instock = true, Price = 13 },
            new Fruit { Id = 25, Name = "Date", Instock = false, Price = 16 },
            new Fruit { Id = 26, Name = "Dragonfruit", Instock = true, Price = 18 },
            new Fruit { Id = 27, Name = "Durian", Instock = false, Price = 22 },
            new Fruit { Id = 28, Name = "Jackfruit", Instock = true, Price = 20 },
            new Fruit { Id = 29, Name = "Kumquat", Instock = false, Price = 8 },
            new Fruit { Id = 30, Name = "Lemon", Instock = true, Price = 5 },
            new Fruit { Id = 31, Name = "Lime", Instock = true, Price = 4 },
            new Fruit { Id = 32, Name = "Mandarin", Instock = false, Price = 7 },
            new Fruit { Id = 33, Name = "Passionfruit", Instock = true, Price = 15 },
            new Fruit { Id = 34, Name = "Pear", Instock = true, Price = 6 },
            new Fruit { Id = 35, Name = "Persimmon", Instock = false, Price = 10 },
            new Fruit { Id = 36, Name = "Quince", Instock = true, Price = 12 },
            new Fruit { Id = 37, Name = "Tangerine", Instock = false, Price = 8 },
            new Fruit { Id = 38, Name = "Mulberry", Instock = true, Price = 11 },
            new Fruit { Id = 39, Name = "Starfruit", Instock = true, Price = 13 },
            new Fruit { Id = 40, Name = "Nectarine", Instock = false, Price = 14 },
            new Fruit { Id = 41, Name = "Clementine", Instock = true, Price = 7 },
            new Fruit { Id = 42, Name = "Soursop", Instock = true, Price = 18 },
            new Fruit { Id = 43, Name = "Salak", Instock = false, Price = 16 },
            new Fruit { Id = 44, Name = "Sapodilla", Instock = true, Price = 12 },
            new Fruit { Id = 45, Name = "Tamarind", Instock = false, Price = 5 },
            new Fruit { Id = 46, Name = "Honeydew", Instock = true, Price = 9 },
            new Fruit { Id = 47, Name = "Gooseberry", Instock = true, Price = 11 },
            new Fruit { Id = 48, Name = "Rambutan", Instock = false, Price = 17 },
            new Fruit { Id = 49, Name = "Loquat", Instock = true, Price = 10 },
            new Fruit { Id = 50, Name = "Pomelo", Instock = true, Price = 20 }
            );
    }
}