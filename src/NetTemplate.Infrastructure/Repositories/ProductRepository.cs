using NetTemplate.Application.Abstractions;
using NetTemplate.Domain.Entities;
using NetTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NetTemplate.Infrastructure.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db) => _db = db;

        public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default) =>
            await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<IReadOnlyList<Product>> ListAsync(CancellationToken ct = default) =>
            await _db.Products.AsNoTracking().OrderByDescending(p => p.Id).ToListAsync(ct);

        public async Task<Product> AddAsync(Product entity, CancellationToken ct = default)
        {
            _db.Products.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }

        public async Task UpdateAsync(Product entity, CancellationToken ct = default)
        {
            _db.Products.Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Product entity, CancellationToken ct = default)
        {
            _db.Products.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default) =>
            _db.Products.AnyAsync(p => p.Id == id, ct);

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }
}
