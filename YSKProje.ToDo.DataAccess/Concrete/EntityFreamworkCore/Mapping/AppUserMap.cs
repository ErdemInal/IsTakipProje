using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFreamworkCore.Mapping
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(I => I.Name).HasMaxLength(100);
            builder.Property(I => I.Surname).HasMaxLength(100);

            builder.HasMany(I => I.Gorevler).WithOne(I => I.AppUser).HasForeignKey(I => I.AppUserId)
                .OnDelete(deleteBehavior: DeleteBehavior.SetNull);
        }
    }
}
