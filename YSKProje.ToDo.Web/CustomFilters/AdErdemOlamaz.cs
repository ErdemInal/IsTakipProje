using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.CustomFilters
{
    public class AdErdemOlamaz : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           var dictioaryGelen = context.ActionArguments.Where(I => I.Key == "model").FirstOrDefault();
           var model = (KullaniciKayitViewModel)dictioaryGelen.Value;
           if (model.Ad.ToLower() == "erdem")
           {
               context.Result = new RedirectResult(@"\Home\Error");
           }
            base.OnActionExecuting(context);
        }
    }
}
