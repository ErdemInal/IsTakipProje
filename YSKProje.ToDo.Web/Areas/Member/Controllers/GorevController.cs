using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles ="Member")]
    [Area("Member")]
    public class GorevController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GorevController(IGorevService gorevService, UserManager<AppUser> userManager, IMapper mapper) : base(userManager)
        {
            _gorevService = gorevService;
            //_userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int aktifSayfa = 1)
        {
            TempData["Active"] = "gorev";
            var user = GetirGirisYapanKullanici();
            int toplamSayfa;
            var gorevler = _gorevService.GetirTumTablolarlaTamamlanmayan(out toplamSayfa, user.Id, aktifSayfa);

            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.AktifSayfa = aktifSayfa;

            var models = _mapper.Map<List<GorevListAllDto>>(gorevler);
            //List<GorevListViewAllModel> models = new List<GorevListViewAllModel>();
            //foreach (var gorev in gorevler)
            //{
            //    GorevListViewAllModel model = new GorevListViewAllModel();
            //    model.Id = gorev.Id;
            //    model.Aciklama = gorev.Aciklama;
            //    model.Aciliyet = gorev.Aciliyet;
            //    model.Ad = gorev.Ad;
            //    model.AppUser = gorev.AppUser;
            //    model.OlusturulmaTarih = gorev.OlusturulmaTarih;
            //    model.Raporlar = gorev.Raporlar;

            //    models.Add(model);
            //}

            return View(models);
        }
    }
}
