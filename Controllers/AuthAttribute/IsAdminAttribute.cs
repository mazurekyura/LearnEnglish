﻿using LearnEnglish.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Controllers.AuthAttribute
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var userService = context
                .HttpContext
                .RequestServices
                .GetService(typeof(UserService)) as UserService;

            if (!userService.IsAdmin())
            {
                context.Result = new ForbidResult();
            }

            base.OnActionExecuted(context);
        }
    }
}
