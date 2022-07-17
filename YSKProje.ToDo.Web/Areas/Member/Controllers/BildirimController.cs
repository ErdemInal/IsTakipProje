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

namespace YSKProje.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles ="Member")]
    [Area("Member")]
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
            TempData["Active"] = "bildirim";
            var user = GetirGirisYapanKullanici();
            var bildirimler = _bildirimService.GetirOkunmayanlar(user.Id);

            var models = _mapper.Map<List<BildirimListDto>>(bildirimler);
            //List<BildirimListViewModel> models = new List<BildirimListViewModel>();

            //foreach (var item in bildirimler)
            //{
            //    BildirimListViewModel model = new BildirimListViewModel();
            //    model.Id = item.Id;
            //    model.Aciklama = item.Aciklama;
            //    models.Add(model);
            //}

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
