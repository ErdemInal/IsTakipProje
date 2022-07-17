using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using AutoMapper;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IsEmriController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDosyaService _dosyaService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;

        public IsEmriController(IAppUserService appUserService, IGorevService gorevService, UserManager<AppUser> userManager,IDosyaService dosyaService, IBildirimService bildirimService, IMapper mapper)
        {
            _appUserService = appUserService;
            _gorevService = gorevService;
            _userManager = userManager;
            _dosyaService = dosyaService;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "isemri";
            //var model = _appUserService.GetirAdminOlmayanlar();

            List<Gorev> gorevler = _gorevService.GetirTumTablolarla();
            var models = _mapper.Map<List<GorevListAllDto>>(gorevler);
            //List<GorevListViewAllModel> models = new List<GorevListViewAllModel>();
            //foreach (var item in gorevler)
            //{
            //    GorevListViewAllModel model = new GorevListViewAllModel();
            //    model.Id = item.Id;
            //    model.Aciklama = item.Aciklama;
            //    model.Aciliyet = item.Aciliyet;
            //    model.Ad = item.Ad;
            //    model.AppUser = item.AppUser;
            //    model.OlusturulmaTarih = item.OlusturulmaTarih;
            //    model.Raporlar = item.Raporlar;
            //    models.Add(model);
            //}

            return View(models);
        }

        public IActionResult Detaylandir(int id)
        {
            TempData["Active"] = "isemri";

            var gorev = _gorevService.GetirRaporlarileId(id);

            var model = _mapper.Map<GorevListAllDto>(gorev);

            //GorevListViewAllModel model = new GorevListViewAllModel();
            //model.Id = gorev.Id;
            //model.Raporlar = gorev.Raporlar;
            //model.Ad = gorev.Ad;
            //model.Aciklama = gorev.Aciklama;
            //model.AppUser = gorev.AppUser;

            return View(model);
        }

        public IActionResult GetirExcel(int id)
        {
            return File(_dosyaService.AktarExcel(_gorevService.GetirRaporlarileId(id).Raporlar),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",Guid.NewGuid()+".xlsx");
        }

        public IActionResult GetirPdf(int id)
        {
            var path = _dosyaService.AktarPdf(_gorevService.GetirRaporlarileId(id).Raporlar);
            return File(path,"application/pdf",Guid.NewGuid()+".pdf");
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

            var appUserListModel = _mapper.Map<List<AppUserListDto>>(personeller);
            //List<AppUserListViewModel> appUserListModel = new List<AppUserListViewModel>(); 
            //foreach (var item in personeller)
            //{
            //    AppUserListViewModel model = new AppUserListViewModel();
            //    model.Id = item.Id;
            //    model.Name = item.Name;
            //    model.Surname = item.Surname;
            //    model.Email = item.Email;
            //    model.Picture = item.Picture;
            //    appUserListModel.Add(model);
            //}

            ViewBag.Personeller = appUserListModel;

           var gorevModel = _mapper.Map<GorevListDto>(gorev);

            //GorevListViewModel gorevModel = new GorevListViewModel();
            //gorevModel.Id = gorev.Id;
            //gorevModel.Ad = gorev.Ad;
            //gorevModel.Aciklama = gorev.Aciklama;
            //gorevModel.Aciliyet = gorev.Aciliyet;
            //gorevModel.OlusturulmaTarih = gorev.OlusturulmaTarih;

            return View(gorevModel);
        }

        //bildirim gonderilecek
        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirDto model)
        {
            var guncellenecekGorev = _gorevService.GetirIdile(model.GorevId);
            guncellenecekGorev.AppUserId = model.PersonelId;
            _gorevService.Guncelle(guncellenecekGorev);

            _bildirimService.Kaydet(new Bildirim
            {
                AppUserId = model.PersonelId,
                Aciklama = $"{guncellenecekGorev.Ad} adlı iş için görevlendirildiniz."
            });

            return RedirectToAction("Index");
        }


        public IActionResult GorevlendirPersonel(PersonelGorevlendirDto model)
        {
            TempData["Active"] = "isemri";
            var user = _userManager.Users.FirstOrDefault(I => I.Id == model.PersonelId);
            var gorev = _gorevService.GetirAciliyetileId(model.GorevId);

            var userModel = _mapper.Map<AppUserListDto>(user);

            //AppUserListViewModel userModel = new AppUserListViewModel();
            //userModel.Id = user.Id;
            //userModel.Name = user.Name;
            //userModel.Picture = user.Picture;
            //userModel.Surname = user.Surname;
            //userModel.Email = user.Email;

            var gorevModel = _mapper.Map<GorevListDto>(gorev);
            //GorevListViewModel gorevModel = new GorevListViewModel();
            //gorevModel.Id = gorev.Id;
            //gorevModel.Aciklama = gorev.Aciklama;
            //gorevModel.Aciliyet = gorev.Aciliyet;
            //gorevModel.Ad = gorev.Ad;

            PersonelGorevlendirListDto personelGorevlendirModel = new PersonelGorevlendirListDto();
            personelGorevlendirModel.AppUser = userModel;
            personelGorevlendirModel.Gorev = gorevModel;
            

            return View(personelGorevlendirModel);
        }
    }
}
