using Microsoft.EntityFrameworkCore;
using WarehouseManager.Models;

namespace WarehouseManager.Repositories
{
	public class AppDbContext : DbContext
	{
		public DbSet<WarehouseModel> Warehouses => Set<WarehouseModel>();
		public DbSet<ProductModel> Products => Set<ProductModel>();

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<WarehouseModel>(entity =>
			{
				entity.HasKey(w => w.Id);
				entity.Property(w => w.Id).ValueGeneratedOnAdd();
				entity.Property(w => w.Name).IsRequired().HasMaxLength(200);

				// Каскадне видалення: видалення складу → видаляються всі його товари
				entity.HasMany(w => w.Products)
					.WithOne(p => p.Warehouse)
					.HasForeignKey(p => p.WarehouseId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<ProductModel>(entity =>
			{
				entity.HasKey(p => p.Id);
				entity.Property(p => p.Id).ValueGeneratedOnAdd();
				entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
				entity.Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
			});
		}
	}
}