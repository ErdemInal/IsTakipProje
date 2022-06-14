using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfAciliyetRepository : EfGenericRepository<Aciliyet>,IAciliyetDal
    {
    }
}
