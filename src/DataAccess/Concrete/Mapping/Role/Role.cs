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

namespace DataAccess.Concrete.Mapping.Role
{
    public partial class Role : AppEntityTypeConfiguration<TRole>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TRole> builder)
        {
            builder.ToTable(nameof(TRole), DbSchemes.Department);

            builder.Property(t => t.Name).IsRequired(true);
            builder.Property(t => t.Description);

            base.Configure(builder);
        }
    }
}