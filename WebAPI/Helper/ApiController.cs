using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Helper
{
    public class ApiController : ControllerBase
    {
        public ApiController() 
        { 

        }

        /// <summary>
        /// Returns Ok when <paramref name="result"/> is not null, otherwise returns NotFound
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult OkFind(object result)
        {
            return result is null ? NotFound() : Ok(result);    
        }
    }
}
