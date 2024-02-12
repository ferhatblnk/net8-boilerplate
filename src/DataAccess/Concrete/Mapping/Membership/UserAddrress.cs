using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Entities.Concrete.UserAddress;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Mapping.Membership
{
    public class UserAddress : AppEntityTypeConfiguration<TUserAddress>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TUserAddress> builder)
        {
            builder.ToTable(nameof(TUserAddress), DbSchemes.Membership);

            builder.Property(t => t.Name);
            builder.Property(t => t.Description);
            base.Configure(builder);
        }
    }
}