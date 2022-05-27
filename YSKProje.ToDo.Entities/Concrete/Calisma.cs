using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class Calisma : ITablo
    {
        //data annotation
        //yapılabilir ama single respons. için bu yanlış
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[Required]
        //[MaxLength(100)]
        public string Ad { get; set; }
        //[Column(TypeName ="ntext")]
        public string Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        //bir calismanın bir kullanıcısı olmak zorunda
        //[ForeignKey("Kullanici")]
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
    }
}
