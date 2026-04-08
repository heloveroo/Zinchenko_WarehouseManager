using Microsoft.EntityFrameworkCore;
using WarehouseManager.Models;
using WarehouseManager.Repositories.Interfaces;

namespace WarehouseManager.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductModel?> GetByIdAsync(int id) =>
            await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<ProductModel> AddAsync(ProductModel product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(ProductModel product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}