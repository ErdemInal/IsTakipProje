﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class BildirimController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBildirimService _bildirimService;
        public BildirimController(UserManager<AppUser> userManager, IBildirimService bildirimService)
        {
            _userManager = userManager;
            _bildirimService = bildirimService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "bildirim";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bildirimler = _bildirimService.GetirOkunmayanlar(user.Id);
            List<BildirimListViewModel> models = new List<BildirimListViewModel>();

            foreach (var item in bildirimler)
            {
                BildirimListViewModel model = new BildirimListViewModel();
                model.Id = item.Id;
                model.Aciklama = item.Aciklama;
                models.Add(model);
            }

            return View(models);
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            var guncellenecekBildirim =_bildirimService.GetirIdile(id);
            guncellenecekBildirim.Durum = true;
            _bildirimService.Guncelle(guncellenecekBildirim);

            return RedirectToAction("Index");
        }
    }
}
