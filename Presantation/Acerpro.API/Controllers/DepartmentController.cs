using Acerpro.API.Controllers.Base;
using Acerpro.Common.DTOs.Department;
using Acerpro.Common.DTOs.Employee;
using Acerpro.Common.Model;
using Acerpro.Model.Entities;
using Acerpro.Service.Repository.Department;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Acerpro.API.Controllers
{
    [Route("department")]
    public class DepartmentController : BaseApiController<DepartmentController>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<WebApiResponse<List<DepartmentResponse>>> List()
        {
            if (ModelState.IsValid)
            {
                var list = await _departmentRepository.GetActive().ToListAsync();

                var response = _mapper.Map<List<DepartmentResponse>>(list);

                return new WebApiResponse<List<DepartmentResponse>>(true, "Succes", response);
            }
            return new WebApiResponse<List<DepartmentResponse>>(false, "Error");
        }

        [HttpGet("{id}")]
        public async Task<WebApiResponse<DepartmentResponse>> Get(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = _mapper.Map<DepartmentResponse>(await _departmentRepository.GetById(id));

                return new WebApiResponse<DepartmentResponse>(true, "Succes", response);
            }
            return new WebApiResponse<DepartmentResponse>(false, "Error");
        }

        [HttpPost]
        public async Task<WebApiResponse<DepartmentResponse>> Add(DepartmentRequest request)
        {
            var entity = _mapper.Map<Department>(request);
            var insertResult = await _departmentRepository.Add(entity);
            if (insertResult != null)
            {
                var rm = _mapper.Map<DepartmentResponse>(insertResult);
                return new WebApiResponse<DepartmentResponse>(true, "Success", rm);
            }
            return new WebApiResponse<DepartmentResponse>(false, "Error");
        }

        [HttpPut]
        public async Task<ActionResult<WebApiResponse<DepartmentResponse>>> Update(DepartmentRequest request)
        {
            try
            {
                var entity = await _departmentRepository.GetById(request.Id);
                if (entity == null)
                    return NotFound();

                _mapper.Map(request, entity);

                var updateResult = await _departmentRepository.Update(entity);
                if (updateResult != null)
                {
                    var rm = _mapper.Map<DepartmentResponse>(updateResult);
                    return new WebApiResponse<DepartmentResponse>(true, "Success", rm);
                }
                return new WebApiResponse<DepartmentResponse>(false, "Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WebApiResponse<DepartmentResponse>>> Delete(Guid id)
        {
            var entity = await _departmentRepository.GetById(id);
            if (entity != null)
            {
                if (await _departmentRepository.Remove(entity))
                    return new WebApiResponse<DepartmentResponse>(true, "Success", _mapper.Map<DepartmentResponse>(entity));
                else
                    return new WebApiResponse<DepartmentResponse>(false, "Error");
            }
            return new WebApiResponse<DepartmentResponse>(false, "Error");
        }
    }
}
