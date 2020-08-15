using AspNetCoreAddin.Data.EF.Extensions;
using AspNetCoreAddin.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreAddin.Data.EF.Configurations
{
    public class FunctionConfiguration : DbEntityConfiguration<Function>
    {
        public override void Configure(EntityTypeBuilder<Function> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().HasMaxLength(255);
        }
    }
}