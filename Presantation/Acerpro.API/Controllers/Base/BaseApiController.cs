using Acerpro.Common.Attributes;
using Acerpro.Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acerpro.API.Controllers.Base
{
    [ValidateModel]
    [Authorize]
    [ApiController]
    public class BaseApiController<T> : ControllerBase where T : BaseApiController<T>
    {
        public BaseApiController()
        {

        }

        private IWorkContext _workContext;

        public IWorkContext WorkContext
        {
            get
            {
                if (_workContext == null)
                {
                    //using Microsoft.Extensions.DependencyInjection;
                    _workContext = HttpContext.RequestServices.GetService<IWorkContext>();
                }
                return _workContext;
            }
            set
            {
                _workContext = value;
            }
        }
    }
}
