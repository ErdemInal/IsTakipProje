using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfRaporRepository : EfGenericRepository<Rapor>, IRaporDal
    {
        public Rapor GetirGorevileId(int id)
        {
            using (var context = new ToDoContext())
            {
                return context.Raporlar.Include(I => I.Gorev).ThenInclude(I=>I.Aciliyet).Where(I => I.Id == id).FirstOrDefault();
            }
        }

        public int GetirRaporSayisiileAppUserId(int id)
        {
            using (var context = new ToDoContext())
            {
                var result = context.Gorevler.Include(I => I.Raporlar).Where(I => I.AppUserId == id);
                return result.SelectMany(I => I.Raporlar).Count();
            }
        }
    }
}
