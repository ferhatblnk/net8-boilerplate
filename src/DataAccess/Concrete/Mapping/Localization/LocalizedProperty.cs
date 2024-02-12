using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Mapping
{
    public partial class LocalizedProperty : AppEntityTypeConfiguration<TLocalizedProperty>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TLocalizedProperty> builder)
        {
            builder.ToTable(nameof(TLocalizedProperty), DbSchemes.Localization);

            builder.HasIndex(t => t.LanguageId);

            builder.Property(t => t.TableName);
            builder.Property(t => t.TableId);
            builder.Property(t => t.TableField);
            builder.Property(t => t.Value);

            builder.HasOne(t => t.Language).WithMany().OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}
