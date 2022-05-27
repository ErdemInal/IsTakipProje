using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.ViewComponents
{
    public class Urunler : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new List<MusteriViewModel>() {
            new MusteriViewModel(){Ad = "Erdem1"},
            new MusteriViewModel(){Ad = "Erdem2"},
            new MusteriViewModel(){Ad = "Erdem3"},
            new MusteriViewModel(){Ad = "Erdem4"}
            });
        }
    }
}
