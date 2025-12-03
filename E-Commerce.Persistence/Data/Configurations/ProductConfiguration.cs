using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("VarChar")
                .HasMaxLength(256);
            builder.Property(p => p.Description)
                .HasColumnType("VarChar")
                .HasMaxLength(1024);
            builder.Property(p => p.PictureUrl)
                .HasColumnType("VarChar")
                .HasMaxLength(256);
            builder.Property(p => p.Price)
                .HasColumnType("Decimal(10,2)");
            
            builder.HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.TypeId);
        }
    }
}
