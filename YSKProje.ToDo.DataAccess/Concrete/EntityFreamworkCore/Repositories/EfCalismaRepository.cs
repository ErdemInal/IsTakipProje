﻿using System;
using System.Collections.Generic;
using System.Linq;
using YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfCalismaRepository : EfGenericRepository<Calisma>,ICalismaDal
    {
        //public List<Calisma> GetirHepsi()
        //{
        //    using var context = new ToDoContext();
        //    return context.Calismalar.ToList();
        //}

        //public Calisma GetirIdile(int id)
        //{
        //    using var context = new ToDoContext();
        //    return context.Calismalar.Find(id);
        //}

        //public void Guncelle(Calisma tablo)
        //{
        //    using (var context = new ToDoContext())
        //    {
        //        context.Calismalar.Update(tablo);
        //        context.SaveChanges();
        //        //context.Entry(tablo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        //context.SaveChanges();
        //    }
        //}

        //public void Kaydet(Calisma tablo)
        //{
        //    using (var context = new ToDoContext())
        //    {
        //        context.Calismalar.Add(tablo);
        //        context.SaveChanges();
        //        //context.Entry(tablo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        //context.SaveChanges();
        //    }
        //}

        //public void Sil(Calisma tablo)
        //{
        //    using (var context = new ToDoContext())
        //    {
        //        context.Calismalar.Remove(tablo);
        //        context.SaveChanges();
        //        //context.Entry(tablo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        //context.SaveChanges();
        //    }
        //}
    }
}
