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

        public IActionResult AtaPersonel(int id,string s, int sayfa=1)
        {
            TempData["Active"] = "isemri";

            ViewBag.AktifSayfa = sayfa;
            //ViewBag.ToplamSayfa = (int)Math.Ceiling((double)_appUserService.GetirAdminOlmayanlar().Count / 3);

            ViewBag.Aranan = s;

            int toplamSayfa;

            var gorev = _gorevService.GetirAciliyetileId(id);
            var personeller = _appUserService.GetirAdminOlmayanlar(out toplamSayfa,s, sayfa);

            ViewBag.ToplamSayfa = toplamSayfa;

            List<AppUserListViewModel> appUserListModel = new List<AppUserListViewModel>(); 
            foreach (var item in personeller)
            {
                AppUserListViewModel model = new AppUserListViewModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.Surname = item.Surname;
                model.Email = item.Email;
                model.Picture = item.Picture;
                appUserListModel.Add(model);
            }

            ViewBag.Personeller = appUserListModel;

            GorevListViewModel gorevModel = new GorevListViewModel();
            gorevModel.Id = gorev.Id;
            gorevModel.Ad = gorev.Ad;
            gorevModel.Aciklama = gorev.Aciklama;
            gorevModel.Aciliyet = gorev.Aciliyet;
            gorevModel.OlusturulmaTarih = gorev.OlusturulmaTarih;

            return View(gorevModel);
        }
    }
}
