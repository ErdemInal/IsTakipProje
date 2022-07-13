using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using YSKProje.ToDo.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IRaporService _raporService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService;
        public HomeController(IRaporService raporService, UserManager<AppUser> userManager, IGorevService gorevService, IBildirimService bildirimService)
        {
            _raporService = raporService;
            _userManager = userManager;
            _gorevService = gorevService;
            _bildirimService = bildirimService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "anasayfa";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
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
