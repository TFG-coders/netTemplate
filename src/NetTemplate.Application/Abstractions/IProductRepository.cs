using NetTemplate.Domain.Entities;

namespace NetTemplate.Application.Abstractions
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Product>> ListAsync(CancellationToken ct = default);
        Task<Product> AddAsync(Product entity, CancellationToken ct = default);
        Task UpdateAsync(Product entity, CancellationToken ct = default);
        Task DeleteAsync(Product entity, CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
