using Microsoft.EntityFrameworkCore;
using WarehouseManager.Models;
using WarehouseManager.Repositories.Interfaces;

namespace WarehouseManager.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WarehouseModel>> GetAllAsync() =>
            await _context.Warehouses
                .Include(w => w.Products)
                .AsNoTracking()
                .ToListAsync();

        public async Task<WarehouseModel?> GetByIdAsync(int id) =>
            await _context.Warehouses
                .Include(w => w.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id);

        public async Task<WarehouseModel> AddAsync(WarehouseModel warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task UpdateAsync(WarehouseModel warehouse)
        {
            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse is not null)
            {
                _context.Warehouses.Remove(warehouse);
                await _context.SaveChangesAsync();
            }
        }
    }
}