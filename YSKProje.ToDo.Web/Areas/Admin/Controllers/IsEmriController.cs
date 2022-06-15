using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IsEmriController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IGorevService _gorevService;

        public IsEmriController(IAppUserService appUserService, IGorevService gorevService)
        {
            _appUserService = appUserService;
            _gorevService = gorevService;

        }
        public IActionResult Index()
        {
            TempData["Active"] = "isemri";
            //var model = _appUserService.GetirAdminOlmayanlar();

            List<Gorev> gorevler = _gorevService.GetirTumTablolarla();
            List<GorevListViewAllModel> models = new List<GorevListViewAllModel>();
            foreach (var item in gorevler)
            {
                GorevListViewAllModel model = new GorevListViewAllModel();
                model.Id = item.Id;
                model.Aciklama = item.Aciklama;
                model.Aciliyet = item.Aciliyet;
                model.Ad = item.Ad;
                model.AppUser = item.AppUser;
                model.OlusturulmaTarih = item.OlusturulmaTarih;
                model.Raporlar = item.Raporlar;
                models.Add(model);
            }

            return View(models);
        }
    }
}
