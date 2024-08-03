using System.ComponentModel.DataAnnotations;

namespace Acerpro.Common.Attributes
{
    public class CustomRequired : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            ErrorMessage = "Lütfen Bu Alanı Giriniz !!!";

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()) ||
                value != null && value.ToString() == "1.01.0001 00:00:00")
                return false;

            return true;
        }
    }
}
