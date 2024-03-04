using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Persistence.EF.Films
{
    public class FilmEntityMap : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_=>_.Name).HasMaxLength(50).IsRequired();
            builder.Property(_=>_.Description);
            builder.Property(_=>_.Director).HasMaxLength(50).IsRequired();
            builder.Property(_=>_.Stock).IsRequired();
            builder.Property(_=>_.MinAgeLimit).IsRequired();
            builder.Property(_=>_.PublishYear).IsRequired();
            builder.Property(_ => _.DailyPriceRent).HasPrecision(10,2).IsRequired();
            builder.Property(_ => _.PenaltyPriceRent).HasPrecision(10,2).IsRequired();
            builder.Property(_ => _.Duration).HasPrecision(10,2).IsRequired();
           

            builder.HasOne(_ => _.Genre)
                .WithMany(_=>_.Films)
                .HasForeignKey(_=>_.GenreId);
        }
    }
}
