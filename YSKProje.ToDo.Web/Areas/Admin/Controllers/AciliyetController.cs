using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using AutoMapper;
using YSKProje.ToDo.DTO.DTOs.AciliyetDtos;
using YSKProje.ToDo.Web.StringInfo;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(AreaInfo.Admin)]
    public class AciliyetController : Controller
    {
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;

        public AciliyetController(IAciliyetService aciliyetService, IMapper mapper)
        {
            _aciliyetService = aciliyetService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.Aciliyet;
            //List<Aciliyet> aciliyetler = _aciliyetService.GetirHepsi();

            //List<AciliyetListViewModel> model = new List<AciliyetListViewModel>();

            //foreach (var item in aciliyetler)
            //{
            //    AciliyetListViewModel aciliyetModel = new AciliyetListViewModel();
            //    aciliyetModel.Id = item.Id;
            //    aciliyetModel.Tanim = item.Tanim;

            //    model.Add(aciliyetModel);
            //}//eski automapper kullanmadan önce
            var model = _mapper.Map<List<AciliyetListDto>>(_aciliyetService.GetirHepsi());
            return View(model);
        }

        public IActionResult EkleAciliyet()
        {
            TempData["Active"] = TempdataInfo.Aciliyet;
            return View(new AciliyetAddDto());
        }

        [HttpPost]
        public IActionResult EkleAciliyet(AciliyetAddDto model)
        {
            if (ModelState.IsValid)
            {
                _aciliyetService.Kaydet(new Aciliyet()
                {
                    Tanim = model.Tanim
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult GuncelleAciliyet(int id)
        {
            TempData["Active"] = TempdataInfo.Aciliyet;
            var aciliyet = _aciliyetService.GetirIdile(id);
            var model = _mapper.Map<AciliyetUpdateDto>(aciliyet);
            //AciliyetUpdateViewModel model = new AciliyetUpdateViewModel()
            //{
            //    Id = aciliyet.Id,
            //    Tanim = aciliyet.Tanim
            //};
            return View(model);
        }

        [HttpPost]
        public IActionResult GuncelleAciliyet(AciliyetUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                _aciliyetService.Guncelle(new Aciliyet()
                {
                    Id = model.Id,
                    Tanim = model.Tanim

                });
                return RedirectToAction("Index");
            }

            return View(model);

        }
    }
}
