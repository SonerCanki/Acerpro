using System.ComponentModel.DataAnnotations;

namespace Acerpro.UI.Models.EmployeeViewModels
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "İsim giriniz")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyisim giriniz")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Title giriniz")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Email giriniz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefon numarası giriniz")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Departman Seçiniz")]
        public Guid DepartmentId { get; set; }
    }
}
