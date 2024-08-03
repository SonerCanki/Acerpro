using System.ComponentModel.DataAnnotations;

namespace Acerpro.UI.Models.DepartmentViewModels
{
    public class DepartmentViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Departman ismi giriniz")]
        public string Name { get; set; }
    }
}
