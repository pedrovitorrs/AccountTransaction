using AccountTransaction.Account.API.Configuration.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccountTransaction.Account.API.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected ActionResult TratarException(Exception ex)
        {
            if (ex.GetType() == typeof(LogicalException))
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
            else
            {
                return NotFound(new { message = JsonConvert.SerializeObject(ex) });
            }
        }
    }
}
