using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.BaseControllers;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class ProfilController : BaseIdentityController
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public ProfilController(UserManager<AppUser> userManager, IMapper mapper):base(userManager)
        {
            //_userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Profil;
            var appUser = await GetirGirisYapanKullanici();

            var model = _mapper.Map<AppUserListDto>(appUser);
            //AppUserListViewModel model = new AppUserListViewModel();
            //model.Id = appUser.Id;
            //model.Name = appUser.Name;
            //model.Surname = appUser.Surname;
            //model.Picture = appUser.Picture;
            //model.Email = appUser.Email;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserListDto model,IFormFile Resim)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekKullanici = _userManager.Users.FirstOrDefault(I => I.Id == model.Id);
                if (Resim != null)
                {
                    string uzanti = Path.GetExtension(Resim.FileName);
                    string resimAd = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + resimAd);
                    using (var straam = new FileStream(path,FileMode.Create))
                    {
                        await Resim.CopyToAsync(straam);
                    }

                    guncellenecekKullanici.Picture = resimAd;
                }

                guncellenecekKullanici.Name = model.Name;
                guncellenecekKullanici.Surname = model.Surname;
                guncellenecekKullanici.Email = model.Email;

                var result = await _userManager.UpdateAsync(guncellenecekKullanici);
                if (result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işleminiz başarı ile gerçekleşti.";
                    return RedirectToAction("Index");
                }

                HataEkle(result.Errors);
            }
            return View(model);
        }
    }
}
