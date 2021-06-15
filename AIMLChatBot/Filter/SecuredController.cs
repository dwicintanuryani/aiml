using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interface;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AIMLChatBot.Filter
{
    public class SecuredController : IActionFilter
    {
        private IUnitofWork _uow { get; set; }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do Something After Method being executed
        }

        public SecuredController(IUnitofWork uow)
        {
            _uow = uow;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var response = new ApiResponseModel();
        }


    }
}
