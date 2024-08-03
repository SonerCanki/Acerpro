using Acerpro.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Acerpro.Common.Attributes
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelStateResponse = new WebApiResponse<bool?>();


                modelStateResponse.ValidationErrors = context.ModelState.Select(x => new ValidationError
                {
                    Key = x.Key,
                    Value = x.Value.Errors.FirstOrDefault()?.ErrorMessage
                }).ToList();

                if (context.ModelState.Any(x => x.Value.Errors.Any(y => y.ErrorMessage == "invalid-id")))
                {
                    context.Result = new BadRequestResult();
                }
                else
                {
                    if (modelStateResponse.ValidationErrors.Any())
                        foreach (var item in modelStateResponse.ValidationErrors)
                            if (!string.IsNullOrWhiteSpace(item.Value))
                            {
                                if (item.Value.Contains("to type 'System.Guid'"))
                                    item.Value = "Please select a item.";
                                else if (item.Value.Contains("is required"))
                                    item.Value = "Please fill required fields.";
                                else if (item.Value.Contains("not a valid e-mail address"))
                                    item.Value = "Invalid email address.";
                                else if (item.Value.Contains("between"))
                                    item.Value = "Please fill a valid text.";
                                else if (item.Value.Contains("System.DateTime"))
                                    item.Value = "Please select a valid date.";
                            }

                    modelStateResponse.ValidationErrors = modelStateResponse.ValidationErrors
                        .Where(x => !string.IsNullOrWhiteSpace(x.Value)).ToList();
                    modelStateResponse.SetErrorMessage("Lütfen Hatalı Alanları Kontrol Ediniz!");
                    context.Result = new OkObjectResult(modelStateResponse);
                }
            }
        }
    }
}
