using Acerpro.Common.DTOs.Department;
using Acerpro.Common.Model;
using Refit;

namespace Acerpro.UI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IDepartmentApi
    {
        [Get("/department")]
        Task<ApiResponse<WebApiResponse<List<DepartmentResponse>>>> List();
        
        [Get("/department/{id}")]
        Task<ApiResponse<WebApiResponse<DepartmentResponse>>> Get(Guid id);

        [Post("/department")]
        Task<ApiResponse<WebApiResponse<DepartmentResponse>>> Add(DepartmentRequest request);

        [Put("/department")]
        Task<ApiResponse<WebApiResponse<DepartmentResponse>>> Update(DepartmentRequest request);

        [Delete("/department/{id}")]
        Task<ApiResponse<WebApiResponse<DepartmentResponse>>> Delete(Guid id);
    }
}
