using Acerpro.API.Controllers.Base;
using Acerpro.Common.DTOs.Employee;
using Acerpro.Common.Enums;
using Acerpro.Common.Model;
using Acerpro.Model.Entities;
using Acerpro.Service.Repository.Employee;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Acerpro.API.Controllers
{
    [Route("employee")]
    public class EmployeeController : BaseApiController<EmployeeController>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<WebApiResponse<List<EmployeeResponse>>> List()
        {
            if (ModelState.IsValid)
            {
                var list = await _employeeRepository.GetDefault(x => x.Status != Status.Deleted, x => x.Department).ToListAsync();

                var response = _mapper.Map<List<EmployeeResponse>>(list);

                return new WebApiResponse<List<EmployeeResponse>>(true, "Succes", response);
            }
            return new WebApiResponse<List<EmployeeResponse>>(false, "Error");
        }

        [HttpGet("{id}")]
        public async Task<WebApiResponse<EmployeeResponse>> Get(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = _mapper.Map<EmployeeResponse>(await _employeeRepository.GetById(id));

                return new WebApiResponse<EmployeeResponse>(true, "Succes", response);
            }
            return new WebApiResponse<EmployeeResponse>(false, "Error");
        }

        [HttpPost]
        public async Task<WebApiResponse<EmployeeResponse>> Add(AddEmployeeRequest request)
        {
            Employee entity = _mapper.Map<Employee>(request);
            var insertResult = await _employeeRepository.Add(entity);
            if (insertResult != null)
            {
                EmployeeResponse rm = _mapper.Map<EmployeeResponse>(insertResult);
                return new WebApiResponse<EmployeeResponse>(true, "Success", rm);
            }
            return new WebApiResponse<EmployeeResponse>(false, "Error");
        }

        [HttpPut]
        public async Task<ActionResult<WebApiResponse<EmployeeResponse>>> Update(UpdateEmployeeRequest request)
        {
            try
            {
                Employee entity = await _employeeRepository.GetById(request.Id);
                if (entity == null)
                    return NotFound();

                _mapper.Map(request, entity);

                var updateResult = await _employeeRepository.Update(entity);
                if (updateResult != null)
                {
                    EmployeeResponse rm = _mapper.Map<EmployeeResponse>(updateResult);
                    return new WebApiResponse<EmployeeResponse>(true, "Success", rm);
                }
                return new WebApiResponse<EmployeeResponse>(false, "Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WebApiResponse<EmployeeResponse>>> Delete(Guid id)
        {
            var entity = await _employeeRepository.GetById(id);
            if (entity != null)
            {
                if (await _employeeRepository.Remove(entity))
                    return new WebApiResponse<EmployeeResponse>(true, "Success", _mapper.Map<EmployeeResponse>(entity));
                else
                    return new WebApiResponse<EmployeeResponse>(false, "Error");
            }
            return new WebApiResponse<EmployeeResponse>(false, "Error");
        }
    }
}
