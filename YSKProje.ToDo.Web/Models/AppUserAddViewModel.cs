using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Models
{
    public class AppUserAddViewDto
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez")]
        [Display(Name = "Kullanıcı Adı:")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Parola boş geçilemez")]
        [Display(Name = "Parola:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage = "Parolalar eşleşmiyor")]
        [Display(Name = "Parolanızı tekrar girin:")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email boş geçilemez")]
        [EmailAddress(ErrorMessage = "Geçersiz Email:")]
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ad boş geçilemez")]
        [Display(Name = "Adınız:")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad boş geçilemez")]
        [Display(Name = "Soyadınız:")]
        public string Surname { get; set; }

    }
}
