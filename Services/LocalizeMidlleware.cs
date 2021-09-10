using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Services
{
    public class LocalizeMidlleware
    {
        private readonly RequestDelegate _next;

        public LocalizeMidlleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userService = context
                .RequestServices.GetService(typeof(UserService)) as UserService;

            var user = userService.GetCurrent();

            if (user == null)
            {
                if (!context.Request.Cookies.Any(x => x.Key == "lang"))
                {
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-EN");
                }
                else
                {
                    var cultureName = context.Request.Cookies["lang"];
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(cultureName);
                }
            }
            else
            {
                switch (user.Language)
                {
                    case EfStuff.Model.Language.Russian:
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
                        break;
                    case EfStuff.Model.Language.English:
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-EN");
                        break;
                    case EfStuff.Model.Language.Polish:
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pl-PL");
                        break;
                }
            }

            await _next.Invoke(context);
        }
    }
}