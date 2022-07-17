using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using YSKProje.ToDo.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class HomeController : BaseIdentityController
    {
        private readonly IRaporService _raporService;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService;
        public HomeController(IRaporService raporService, UserManager<AppUser> userManager, IGorevService gorevService, IBildirimService bildirimService) : base(userManager)
        {
            _raporService = raporService;
            //_userManager = userManager;
            _gorevService = gorevService;
            _bildirimService = bildirimService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "anasayfa";
            var user = GetirGirisYapanKullanici();
            var raporSayisi = _raporService.GetirRaporSayisiileAppUserId(user.Id);
            var tamamlananGorevSayisi = _gorevService.GetirGorevSayisiTamamlananileAppUserId(user.Id);
            var bildirim = _bildirimService.GetirOkunmayanBildirimSayisiileAppUserId(user.Id);
            var tamamlananmayanGorevSayisi = _gorevService.GetirGörevSayisiTamamlanmasiGerekenAppUserId(user.Id);

            ViewBag.RaporSayisi = raporSayisi;

            ViewBag.TamamlananGorevSayisi = tamamlananGorevSayisi;

            ViewBag.BildirimSayisi = bildirim;

            ViewBag.TamamlananmayanGorevSayisi = tamamlananmayanGorevSayisi;

            return View();
        }
    }
}
