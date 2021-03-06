using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DTO.DTOs.BildirimDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class BildirimController : BaseIdentityController
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public BildirimController(UserManager<AppUser> userManager, IBildirimService bildirimService, IMapper mapper) : base(userManager)
        {
            //_userManager = userManager;
            _bildirimService = bildirimService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Bildirim;
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = await GetirGirisYapanKullanici();
            var bildirimler = _bildirimService.GetirOkunmayanlar(user.Id);

            //List<BildirimListViewModel> models = new List<BildirimListViewModel>();

            //foreach (var item in bildirimler)
            //{
            //    BildirimListViewModel model = new BildirimListViewModel();
            //    model.Id = item.Id;
            //    model.Aciklama = item.Aciklama;
            //    models.Add(model);
            //}
            var models = _mapper.Map<List<BildirimListDto>>(bildirimler);

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
