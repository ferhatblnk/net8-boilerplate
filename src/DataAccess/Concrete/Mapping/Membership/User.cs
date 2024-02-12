using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Core.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Concrete.Mapping
{
    public partial class User : AppEntityTypeConfiguration<TUser>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TUser> builder)
        {
            builder.ToTable(nameof(TUser), DbSchemes.Membership);

            builder.Property(t => t.FirstName);
            builder.Property(t => t.LastName);
            builder.Property(t => t.Email);
            builder.Property(t => t.Password);
            builder.Property(t => t.Token);
            builder.Property(t => t.TokenExpiredAt).IsRequired(false);


            base.Configure(builder);
        }
    }
}
