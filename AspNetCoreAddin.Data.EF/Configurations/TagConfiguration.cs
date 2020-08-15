using AspNetCoreAddin.Data.EF.Extensions;
using AspNetCoreAddin.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAddin.Data.EF.Configurations
{
    public class TagConfiguration : DbEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().HasColumnType("varchar(50)");
        }
    }
}
