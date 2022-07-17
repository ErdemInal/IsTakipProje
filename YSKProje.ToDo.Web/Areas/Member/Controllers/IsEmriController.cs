﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.DTO.DTOs.RaporDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles="Member")]
    [Area("Member")]
    public class IsEmriController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IRaporService _raporService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;

        public IsEmriController(IGorevService gorevService, UserManager<AppUser> userManager, IRaporService raporService, IBildirimService bildirimService, IMapper mapper):base(userManager)
        {
            _gorevService = gorevService;
            //_userManager = userManager;
            _raporService = raporService;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "isemri";

            var user = await GetirGirisYapanKullanici();

            var gorevler = _gorevService.GetirTumTablolarla(I => I.AppUserId == user.Id && !I.Durum);

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
            //    model.Raporlar = item.Raporlar;
            //    model.OlusturulmaTarih = item.OlusturulmaTarih;
            //    models.Add(model);

            //}


            return View(models);
        }

        public IActionResult EkleRapor(int id)
        {
            TempData["Active"] = "isemri";

            var gorev = _gorevService.GetirAciliyetileId(id);
            var model = _mapper.Map<RaporAddDto>(gorev);
            //RaporAddViewModel model = new RaporAddViewModel();
            //model.GorevId = id;
            //model.Gorev = gorev;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EkleRapor(RaporAddDto model)
        {
            if (ModelState.IsValid)
            {
                _raporService.Kaydet(new Rapor()
                {
                    GorevId = model.GorevId,
                    Detay = model.Detay,
                    Tanim = model.Tanim
                });

                var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
                var aktifKullanici = await GetirGirisYapanKullanici();

                foreach (var admin in adminUserList)
                {
                    _bildirimService.Kaydet(new Bildirim
                    {
                        Aciklama = $"{aktifKullanici.Name} {aktifKullanici.Surname} yeni bir rapor yazdı.",
                        AppUserId = admin.Id

                    });
                }

                return RedirectToAction("Index");
            }



            return View(model);
        }

        public IActionResult GuncelleRapor(int id)
        {
            TempData["Active"] = "isemri";

            var rapor = _raporService.GetirGorevileId(id);
            var model = _mapper.Map<RaporUpdateDto>(rapor);
            //RaporUpdateViewModel model = new RaporUpdateViewModel();
            //model.Id = rapor.Id;
            //model.Tanim = rapor.Tanim;
            //model.Detay = rapor.Detay;
            //model.Gorev = rapor.Gorev;
            //model.GorevId = rapor.GorevId;

            return View(model);
        }

        [HttpPost]
        public IActionResult GuncelleRapor(RaporUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekRapor = _raporService.GetirIdile(model.Id);
                guncellenecekRapor.GorevId = model.GorevId;
                guncellenecekRapor.Tanim = model.Tanim;
                guncellenecekRapor.Detay = model.Detay;

                _raporService.Guncelle(guncellenecekRapor);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> TamamlaGorev(int gorevId)
        {
            var guncellenecekGorev = _gorevService.GetirIdile(gorevId);
            guncellenecekGorev.Durum = true;
            _gorevService.Guncelle(guncellenecekGorev);

            var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
            var aktifKullanici = await GetirGirisYapanKullanici();

            foreach (var admin in adminUserList)
            {
                _bildirimService.Kaydet(new Bildirim
                {
                    Aciklama = $"{aktifKullanici.Name} {aktifKullanici.Surname} vermiş olduğunuz görevi tamamladı.",
                    AppUserId = admin.Id

                });
            }

            return Json(null);
        }

    }
}
