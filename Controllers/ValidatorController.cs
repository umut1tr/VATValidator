using Microsoft.AspNetCore.Mvc;
using VATValidator.Models;

namespace VATValidator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ValidatorController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post(VatRequest vatRequest)
        {            

            return base.Ok(VatUtils.checkVat(vatRequest.vat));
        }
    }
}