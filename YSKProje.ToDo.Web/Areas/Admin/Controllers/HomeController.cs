using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using YSKProje.ToDo.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRaporService _raporService;

        public HomeController(IGorevService gorevService, IBildirimService bildirimService, UserManager<AppUser> userManager, IRaporService raporService)
        {
            _gorevService = gorevService;
            _bildirimService = bildirimService;
            _userManager = userManager;
            _raporService = raporService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "anasayfa";

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var atanmayiBekleyenGorev = _gorevService.GetirAtanmayiBekleyenGorevSayisi();
            var tamamlanmısGorev = _gorevService.GetirGorevTamamlanmıs();
            var okunmamisBildirim = _bildirimService.GetirOkunmayanBildirimSayisiileAppUserId(user.Id);
            var raporSayisi = _raporService.GetirRaporSayisi();


            ViewBag.AtanmayiBekleyenGorev = atanmayiBekleyenGorev;
            ViewBag.TamamlanmisGorev = tamamlanmısGorev;
            ViewBag.OkunmamisBildirim = okunmamisBildirim;
            ViewBag.RaporSayisi = raporSayisi;

            return View();
        }

        
    }
}
