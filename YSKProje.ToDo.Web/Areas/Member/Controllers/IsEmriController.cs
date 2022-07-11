using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class IsEmriController : Controller
    {
        private readonly IGorevService _gorevService;

        public IsEmriController(IGorevService gorevService)
        {
            _gorevService = gorevService;
        }
        public IActionResult Index(int id)
        {
            TempData["Active"] = "isemri";
            var gorevler = _gorevService.GetirTumTablolarla(I => I.AppUserId == id && !I.Durum);

            List<GorevListViewAllModel> models = new List <GorevListViewAllModel>();

            foreach (var item in gorevler)
            {
                GorevListViewAllModel model = new GorevListViewAllModel();
                model.Id = item.Id;
                model.Aciklama = item.Aciklama;
                model.Aciliyet = item.Aciliyet;
                model.Ad = item.Ad;
                model.AppUser = item.AppUser;
                model.Raporlar = item.Raporlar;
                model.OlusturulmaTarih = item.OlusturulmaTarih;
                models.Add(model);

            }
            

            return View(models);
        }
    }
}
