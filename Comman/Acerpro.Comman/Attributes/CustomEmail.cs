using Acerpro.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Acerpro.Common.Attributes
{
    public class CustomEmail : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            ErrorMessage = "Lütfen Uygun Mail Adresi Giriniz!!!";

            if (value != null && !value.ToString().IsValidEmailAddress()) return false;

            return true;
        }
    }
}
