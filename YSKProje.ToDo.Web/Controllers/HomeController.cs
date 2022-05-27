using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using YSKProje.ToDo.Web.CustomExtensions;
using YSKProje.ToDo.Web.CustomFilters;
using YSKProje.ToDo.Web.CustomLogger;
using YSKProje.ToDo.Web.Models;

namespace YSKProje.ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //class Barcodes
        //{
        //    public string Barcode { get; set; }
        //}
        public IActionResult Index()
        {
            ViewBag.Isim = "xxx";
            TempData["Isim"] = "Erdem";
            ViewData["Isim"] = "xxx";

            //string barcode2 = "";
            //List<Barcodes> barcode = new List<Barcodes>();

            //barcode.Add(new Barcodes() { Barcode = "123456789atacinarinal"});
            //foreach (var item in barcode)
            //{
            //    barcode2 = item.Barcode;
            //    var t = barcodelar(barcode2);
            //    tt = t.ToString();
            //}

            //ViewBag.Barcode = tt;
            SetCookie();
            ViewBag.Cookie = GetCookie();

            SetSession();
            ViewBag.Cookie = GetSession();

            


            return View(); 
        }

        
        //public string barcodelar(string input, char maskChar = '*')
        //{
        //    // If the string is 2 characters or less, just return it
        //    if (string.IsNullOrEmpty(input) || input.Length < 3) return input;

        //    // Get the length and start position of mask
        //    var maskLength = input.Length / 2;
        //    var maskStart = (input.Length - maskLength) / 2;

        //    // Adjust mask length so there's an even number of characters 
        //    // showing before and after it (change -- to ++ for more mask)
        //    if (input.Length % 2 != maskLength % 2) maskLength--;

        //    var x = input.Substring(0, maskStart) +           // First part
        //           new string(maskChar, maskLength) +        // Mask part
        //           input.Substring(maskStart + maskLength);  // Last part
        //    return x;
        //}

        public IActionResult KayitOl()
        {
            return View();
        }
        [AdErdemOlamaz]
        [HttpPost]
        public IActionResult KayitOl2(KullaniciKayitViewModel model)
        {
            //string ad = HttpContext.Request.Form["Ad"].ToString();
            //ViewBag.Ad = ad;
            //server side validation
            //jquery validateler eklendi client side için
            if (ModelState.IsValid)
            {
                
            }
            ModelState.AddModelError(nameof(KullaniciKayitViewModel.Ad),"Ad alanı gereklidir");
            ModelState.AddModelError("","Modelle ilgili hata");
            return View("KayitOl",model);
        }

        public void SetCookie()
        {
            HttpContext.Response.Cookies.Append("kisi","erdem",new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(20),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true // https
            });
        }

        public string GetCookie()
        {
            return HttpContext.Request.Cookies["kisi"];
        }

        public void SetSession()
        {
            HttpContext.Session.SetObject("kisi",new KullaniciKayitViewModel(){Ad = "Erdem",Soyad = "İnal"});
            //HttpContext.Session.SetString("kisi","Erdem");
        }

        //public string GetSession()
        public KullaniciKayitViewModel GetSession()
        {
            return HttpContext.Session.GetObject<KullaniciKayitViewModel>("kisi");
            //return HttpContext.Session.GetString("kisi");
        }

        public IActionResult PageError(int code)
        {
            ViewBag.Code = code;
            if (code == 404)
            {
                ViewBag.Message = "Sayfa Bulunamadı";
            }
            return View();
        }

        public IActionResult Hata()
        {
             throw new Exception("Hata Oluştu");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            CustomLogger.NLogLogger nLogLogger = new NLogLogger();
            nLogLogger.LogWithNLog($"hatanın oluştuğu yer {exceptionPathFeature.Path} \n hata mesajı : {exceptionPathFeature.Error.Message} \n stack trace : {exceptionPathFeature.Error.StackTrace}");

            ViewBag.Path = exceptionPathFeature.Path;
            ViewBag.Message = exceptionPathFeature.Error.Message;
            return View();

            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
