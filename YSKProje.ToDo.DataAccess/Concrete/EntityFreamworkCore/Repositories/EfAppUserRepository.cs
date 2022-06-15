using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfAppUserRepository : IAppUserDal
    {
        public List<AppUser> GetirAdminOlmayanlar()
        {
            using (var context = new ToDoContext())
            {
                return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId,
                     (resultUser, resultUserRole) => new
                     {
                         user = resultUser,
                         userRole = resultUserRole
                     }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
                     (resultTable, resultRole) => new
                     {
                         user = resultTable.user,
                         userRoles = resultTable.userRole,
                         roles = resultRole
                     }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
                     {
                         Id = I.user.Id,
                         Name = I.user.Name,
                         Surname = I.user.Surname,
                         Picture = I.user.Picture,
                         Email = I.user.Email,
                         UserName = I.user.UserName
                     }).ToList();
            }
        }
    }
}
