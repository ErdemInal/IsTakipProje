using System;
using System.Collections.Generic;
using System.Linq;
using YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfKullaniciRepository : EfGenericRepository<Kullanici>, IKullaniciDal
    {
        //Generic olmazsa tekrara düşüyoruz
        //public List<Kullanici> GetirHepsi()
        //{
        //    using (var context = new ToDoContext())
        //    {
        //       return context.Kullanicilar.ToList();
        //    }
        //}

        //public Kullanici GetirIdile(int id)
        //{
        //    using (var context = new ToDoContext())
        //    {
        //        return context.Kullanicilar.Find(id);
        //    }
        //}

        //public void Guncelle(Kullanici tablo)
        //{
        //    using (var context = new ToDoContext())
        //    {
        //        context.Kullanicilar.Update(tablo);
        //        context.SaveChanges();
        //    }
        //}

        //public void Kaydet(Kullanici tablo)
        //{
        //    using (var context = new ToDoContext())
        //    {
        //        context.Kullanicilar.Add(tablo);
        //        context.SaveChanges();
        //    }
        //}

        //public void Sil(Kullanici tablo)
        //{
        //    using (var context = new ToDoContext())
        //    {
        //        context.Kullanicilar.Remove(tablo);
        //    }
        //}
    }
}
