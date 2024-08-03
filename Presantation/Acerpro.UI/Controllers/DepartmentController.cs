using Acerpro.Common.DTOs.Department;
using Acerpro.UI.APIs;
using Acerpro.UI.Models.DepartmentViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acerpro.UI.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IDepartmentApi _departmentApi;

        public DepartmentController(IMapper mapper, IDepartmentApi departmentApi)
        {
            _mapper = mapper;
            _departmentApi = departmentApi;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<DepartmentViewModel>();
            var listResult = await _departmentApi.List();

            if (listResult.IsSuccessStatusCode && listResult.Content.IsSuccess && listResult.Content.ResultData.Any())
                list = _mapper.Map<List<DepartmentViewModel>>(listResult.Content.ResultData);

            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Insert(AddDepartmentViewModel item)
        {
            if (ModelState.IsValid)
            {
                var a = _mapper.Map<DepartmentRequest>(item);
                var insertResult = await _departmentApi.Add(a);
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
            var model = new DepartmentViewModel();
            var updateResult = await _departmentApi.Get(id);
            if (updateResult.IsSuccessStatusCode && updateResult.Content.IsSuccess && updateResult.Content.ResultData != null)
                model = _mapper.Map<DepartmentViewModel>(updateResult.Content.ResultData);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(DepartmentViewModel item)
        {
            if (ModelState.IsValid)
            {
                var a = _mapper.Map<DepartmentRequest>(item);
                var updateResult = await _departmentApi.Update(a);
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
            var deletedResut = await _departmentApi.Delete(id);
            if (deletedResut.IsSuccessStatusCode && deletedResut.Content.IsSuccess && deletedResut.Content.ResultData != null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
