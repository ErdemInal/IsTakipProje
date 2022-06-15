using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;

        public GorevController(IGorevService gorevService)
        {
            _gorevService = gorevService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "gorev";
            List<Gorev> gorevler = _gorevService.GetirHepsi();
            List<GorevListViewModel> models = new List<GorevListViewModel>();

            foreach (var item in gorevler)
            {
                GorevListViewModel model = new GorevListViewModel
                {
                    Aciklama = item.Aciklama,
                    Aciliyet = item.Aciliyet,
                    AciliyetId = item.AciliyetId,
                    Ad = item.Ad,
                    Durum = item.Durum,
                    Id = item.Id,
                    OlusturulmaTarih = item.OlusturulmaTarih
                };
                models.Add(model);

            }
            return View(models);
        }
    }
}
