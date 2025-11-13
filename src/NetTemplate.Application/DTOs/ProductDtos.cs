

namespace NetTemplate.Application.DTOs
{
    public record ProductCreateRequest(string Name, decimal Price, bool InStock);
    public record ProductUpdateRequest(string Name, decimal Price, bool InStock);
    public record ProductResponse(int Id, string Name, decimal Price, bool InStock);
}
