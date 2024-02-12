using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Mapping.Department
{
    public partial class Department : AppEntityTypeConfiguration<TDepartment>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TDepartment> builder)
        {
            builder.ToTable(nameof(TDepartment), DbSchemes.Department);

            builder.Property(t => t.Budget);
            builder.Property(t => t.Description);
            builder.Property(t => t.Email);
            builder.Property(t => t.Location);
            builder.Property(t => t.Name);
            builder.Property(t => t.PhoneNumber);

            base.Configure(builder);
        }
    }
}