using Microsoft.EntityFrameworkCore;

namespace DynamicLinqSelectSample
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }

    }

    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ECommerceContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Server=localhost,32774;Database=Loscm;User=sa;Password=MyPassword001;");
        }
    }
}