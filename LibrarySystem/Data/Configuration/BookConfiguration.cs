using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Configuration;

public class BookConfiguration:IEntityTypeConfiguration<Book>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
    {


        builder.Property(b => b.Price)
                .IsRequired()
                .HasPrecision(18, 2);


        builder.Property(b => b.Stock)
               .IsRequired();
            
    }
}
