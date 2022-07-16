using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class AppUserSignInDto
    {
        //[Required(ErrorMessage = "Kullanıcı Adı boş geçilemez")]
        //[Display(Name = "Kullanıcı Adı:")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Parola boş geçilemez")]
        //[Display(Name = "Parola:")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "Beni Hatırla ")]
        public bool RememberMe { get; set; }
    }
}
