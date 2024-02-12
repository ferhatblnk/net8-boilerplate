using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Core.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Mapping
{
    public partial class LocalizedMap : AppEntityTypeConfiguration<TLocalizedMap>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TLocalizedMap> builder)
        {
            builder.ToTable(nameof(TLocalizedMap), DbSchemes.Localization);

            builder.HasIndex(t => t.LanguageId);
            builder.HasIndex(t => t.GroupCode).IsUnique(false);

            builder.Property(t => t.GroupCode).IsRequired(false);
            builder.Property(t => t.MapKey);
            builder.Property(t => t.Value);

            builder.HasOne(t => t.Language).WithMany().OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}
