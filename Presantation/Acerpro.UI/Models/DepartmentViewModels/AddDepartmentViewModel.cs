using System.ComponentModel.DataAnnotations;

namespace Acerpro.UI.Models.DepartmentViewModels
{
    public class AddDepartmentViewModel
    {
        [Required(ErrorMessage = "Departman ismi giriniz")]
        public string Name { get; set; }
    }
}
