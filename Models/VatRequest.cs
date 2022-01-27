using System.ComponentModel.DataAnnotations;

namespace VATValidator.Controllers
{
    public partial class ValidatorController
    {
        public class VatRequest
        {
            [Required]
            public string vat { get; set; }
        }
    }
}