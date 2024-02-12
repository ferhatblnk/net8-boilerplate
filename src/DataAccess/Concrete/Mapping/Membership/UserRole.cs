using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Entities.Concrete.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Mapping.Membership
{
    public partial class User : AppEntityTypeConfiguration<TUserRole>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TUserRole> builder)
        {
            builder.ToTable(nameof(TUserRole), DbSchemes.Membership);

            builder.HasKey(t => new { t.UserId, t.RoleId });

            builder.Property(t => t.UserId);
            builder.Property(t => t.RoleId);

            builder.HasOne(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(ur => ur.UserId);

            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            base.Configure(builder);
        }
    }
}