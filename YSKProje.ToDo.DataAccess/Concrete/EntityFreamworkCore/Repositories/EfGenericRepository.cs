using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfGenericRepository<Tablo> : IGenericDal<Tablo> where Tablo : class, ITablo, new()
    {
        public List<Tablo> GetirHepsi()
        {
            using (var context = new ToDoContext())
            {
                return context.Set<Tablo>().ToList();
            }
        }

        public Tablo GetirIdile(int id)
        {
            using (var context = new ToDoContext())
            {
                return context.Set<Tablo>().Find(id);
            }
        }

        public void Guncelle(Tablo tablo)
        {
            using (var context = new ToDoContext())
            {
                context.Set<Tablo>().Update(tablo);
                context.SaveChanges();
            }
        }

        public void Kaydet(Tablo tablo)
        {
            using (var context = new ToDoContext())
            {
                context.Set<Tablo>().Add(tablo);
                context.SaveChanges();
            }
        }

        public void Sil(Tablo tablo)
        {
            using (var context = new ToDoContext())
            {
                context.Set<Tablo>().Remove(tablo);
                context.SaveChanges();
            }
        }
    }
}
