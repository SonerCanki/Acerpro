using Acerpro.Common.DTOs.Employee;
using Acerpro.Common.Model;
using Refit;

namespace Acerpro.UI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IEmployeeApi
    {
        [Get("/employee")]
        Task<ApiResponse<WebApiResponse<List<EmployeeResponse>>>> List();
        
        [Get("/employee/{id}")]
        Task<ApiResponse<WebApiResponse<EmployeeResponse>>> Get(Guid id);

        [Post("/employee")]
        Task<ApiResponse<WebApiResponse<EmployeeResponse>>> Add(AddEmployeeRequest request);

        [Put("/employee")]
        Task<ApiResponse<WebApiResponse<EmployeeResponse>>> Update(UpdateEmployeeRequest request);

        [Delete("/employee/{id}")]
        Task<ApiResponse<WebApiResponse<EmployeeResponse>>> Delete(Guid id);
    }
}
