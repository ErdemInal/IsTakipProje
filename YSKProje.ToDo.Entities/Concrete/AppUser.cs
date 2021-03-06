using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class AppUser : IdentityUser<int>,ITablo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Picture { get; set; } = "default.png";
        public List<Bildirim> Bildirimler { get; set; }//1 user birden fazla bildirim alabilir
        public List<Gorev> Gorevler { get; set; } //1 user birden fazla görev alabilir
    }
}
