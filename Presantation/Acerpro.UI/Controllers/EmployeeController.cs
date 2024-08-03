using Acerpro.Common.DTOs.Employee;
using Acerpro.UI.APIs;
using Acerpro.UI.Models.DepartmentViewModels;
using Acerpro.UI.Models.EmployeeViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Acerpro.UI.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IEmployeeApi _employeeApi;
        private readonly IDepartmentApi _departmentApi;

        public EmployeeController(IMapper mapper,
            IEmployeeApi employeeApi,
            IDepartmentApi departmentApi)
        {
            _mapper = mapper;
            _employeeApi = employeeApi;
            _departmentApi = departmentApi;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<EmployeeViewModel>();
            var listResult = await _employeeApi.List();

            if (listResult.IsSuccessStatusCode && listResult.Content.IsSuccess && listResult.Content.ResultData.Any())
                list = _mapper.Map<List<EmployeeViewModel>>(listResult.Content.ResultData);

            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var listDepartments = _mapper.Map<List<DepartmentViewModel>>((await _departmentApi.List()).Content.ResultData);
            ViewBag.Departments = new SelectList(listDepartments, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Insert(AddEmployeeViewModel item)
        {
            if (ModelState.IsValid)
            {
                var insertResult = await _employeeApi.Add(_mapper.Map<AddEmployeeRequest>(item));
                if (insertResult.IsSuccessStatusCode && insertResult.Content.IsSuccess && insertResult.Content.ResultData != null)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Insert", item);
        }
        
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var listDepartments = _mapper.Map<List<DepartmentViewModel>>((await _departmentApi.List()).Content.ResultData);
            ViewBag.Departments = new SelectList(listDepartments, "Id", "Name");

            var model = new UpdateEmployeeViewModel();
            var updateResult = await _employeeApi.Get(id);
            if (updateResult.IsSuccessStatusCode && updateResult.Content.IsSuccess && updateResult.Content.ResultData != null)
                model = _mapper.Map<UpdateEmployeeViewModel>(updateResult.Content.ResultData);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel item)
        {
            if (ModelState.IsValid)
            {
                var a = _mapper.Map<UpdateEmployeeRequest>(item);
                var updateResult = await _employeeApi.Update(a);
                if (updateResult.IsSuccessStatusCode && updateResult.Content.IsSuccess && updateResult.Content.ResultData != null)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedResut = await _employeeApi.Delete(id);
            if (deletedResut.IsSuccessStatusCode && deletedResut.Content.IsSuccess && deletedResut.Content.ResultData != null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
