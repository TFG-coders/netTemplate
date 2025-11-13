using Microsoft.AspNetCore.Mvc;
using NetTemplate.Application.Abstractions;
using NetTemplate.Application.DTOs;
using NetTemplate.Domain.Entities;

namespace NetTemplate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo) => _repo = repo;

        /// <summary> GET /api/products </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> List(CancellationToken ct)
        {
            var items = await _repo.ListAsync(ct);
            var dto = items.Select(p => new ProductResponse(p.Id, p.Name, p.Price, p.InStock));
            return Ok(dto);
        }

        /// <summary> GET /api/products/{id} </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResponse>> Get(int id, CancellationToken ct)
        {
            var p = await _repo.GetByIdAsync(id, ct);
            if (p is null) return NotFound();
            return Ok(new ProductResponse(p.Id, p.Name, p.Price, p.InStock));
        }

        /// <summary> POST /api/products </summary>
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductCreateRequest req, CancellationToken ct)
        {
            var entity = new Product
            {
                Name = req.Name.Trim(),
                Price = req.Price,
                InStock = req.InStock
            };
            await _repo.AddAsync(entity, ct);
            return CreatedAtAction(nameof(Get), new { id = entity.Id },
                new ProductResponse(entity.Id, entity.Name, entity.Price, entity.InStock));
        }

        /// <summary> PUT /api/products/{id} </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateRequest req, CancellationToken ct)
        {
            var exists = await _repo.ExistsAsync(id, ct);
            if (!exists) return NotFound();

            var entity = new Product
            {
                Id = id,
                Name = req.Name.Trim(),
                Price = req.Price,
                InStock = req.InStock
            };

            await _repo.UpdateAsync(entity, ct);
            return NoContent();
        }

        /// <summary> DELETE /api/products/{id} </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return NotFound();
            await _repo.DeleteAsync(entity, ct);
            return NoContent();
        }
    }
}