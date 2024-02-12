using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Mapping
{
    public partial class SystemLog : AppEntityTypeConfiguration<TSystemLog>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TSystemLog> builder)
        {
            builder.ToTable(nameof(TSystemLog), DbSchemes.Log);

            builder.HasIndex(t => t.LogUserId).IsUnique(false);
            builder.HasIndex(t => t.GroupId).IsUnique(false);

            builder.Property(t => t.Endpoint);
            builder.Property(t => t.Request);
            builder.Property(t => t.Response);
            builder.Property(t => t.Detail);

            base.Configure(builder);
        }
    }
}
