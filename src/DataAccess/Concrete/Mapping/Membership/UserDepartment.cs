using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Entities.Concrete;
using Entities.Concrete.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Mapping.Membership
{
    public partial class UserDepartment : AppEntityTypeConfiguration<TUserDepartment>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TUserDepartment> builder)
        {
            builder.ToTable(nameof(TUserDepartment), DbSchemes.Membership);

            builder.HasKey(t => new { t.UserId, t.DepartmentId });

            builder.Property(t => t.UserId);
            builder.Property(t => t.DepartmentId);

            builder.HasOne(ur => ur.User)
                        .WithMany(u => u.UserDepartments)
                        .HasForeignKey(ur => ur.UserId);

            builder.HasOne(ur => ur.Department)
                .WithMany(u => u.UserDepartments)
                .HasForeignKey(ur => ur.DepartmentId);
                
            base.Configure(builder);
        }
    }
}
