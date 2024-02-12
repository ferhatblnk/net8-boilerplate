using Core.Constants;
using Core.DataAccess.Abstract.Mapping;
using Core.DataAccess.Concrete.Mapping;
using Core.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Mapping
{
    public partial class Language : AppEntityTypeConfiguration<TLanguage>, IMapping
    {
        public override void Configure(EntityTypeBuilder<TLanguage> builder)
        {
            builder.ToTable(nameof(TLanguage), DbSchemes.Localization);
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.RowGuid);
            builder.Property(t => t.Name);
            builder.Property(t => t.LanguageCode);
            builder.Property(t => t.FlagUrl);

            base.Configure(builder);
        }
    }
}
