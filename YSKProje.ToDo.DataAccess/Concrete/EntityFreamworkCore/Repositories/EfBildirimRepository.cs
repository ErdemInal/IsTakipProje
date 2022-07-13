using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimDal
    {
        public int GetirOkunmayanBildirimSayisiileAppUserId(int AppUserId)
        {
            using (var context = new ToDoContext())
            {
                return context.Bildirimler.Count(I => I.AppUserId == AppUserId && !I.Durum);
            }
        }

        public List<Bildirim> GetirOkunmayanlar(int AppUserId)
        {
            using (var context = new ToDoContext())
            {
                return context.Bildirimler.Where(I => I.AppUserId == AppUserId && !I.Durum).ToList();
            }
        }
    }
}
