using Microsoft.EntityFrameworkCore;
using P010Store.Entities;

namespace P010Store.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=DESKTOP-O97PCTN\\SQLEXPRESS;Database=P010Store;Trusted_Connection=True;TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer(@"Server=mssql.siteadi.com veya 84.118.123.25; database=P010Store; username=veritabanikullaniciadi; password=veritabanisifresi"); // Canlı ayarları için connection string den : buna benzer bi şey olur  


            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Phone = "",
                    Email = "admin@projeuygulamasi.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "admin",
                    Surname = "admin",
                    Password = "123"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
