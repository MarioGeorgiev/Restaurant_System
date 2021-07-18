using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restourant.Data.Drinks;
using Restourant.Data.Foods;
using Restourant.Data.MappingTables;
using Restourant.Data.Models.Sold;
using Restourant.Data.Sold;
using Restourant.Data.Tables;
using Restourant.Data.User;
using Restourant.Models;
namespace Restourant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TableDrinks> TableDrinks { get; set; }
        public DbSet<TableFoods> TableFoods { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<Drink> Drinks { get; set; }

        public DbSet<FoodSold> FoodsSold { get; set; }

        public DbSet<DrinkSold> DrinksSold { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.configurationString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableDrinks>()
                  .HasKey(td => new { td.DrinkId, td.TableId });

            modelBuilder.Entity<TableFoods>()
                .HasKey(tf => new { tf.FoodId, tf.TableId });
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => p.UserId);
        }

        public DbSet<Restourant.Models.AddDrinkViewModel> AddDrinkViewModel { get; set; }

        public DbSet<Restourant.Models.AddFoodViewModel> AddFoodViewModel { get; set; }


    }
}
