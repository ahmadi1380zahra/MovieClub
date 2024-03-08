using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Persistence.EF.Rents
{
    public class RentEntityMap : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.RentAt).IsRequired();
            builder.Property(_ => _.GiveBackAt);
            builder.Property(_ => _.FilmRate).HasPrecision(18, 2).IsRequired();
            builder.Property(_ => _.FilmDailyPrice).HasPrecision(18, 2).IsRequired();
            builder.Property(_ => _.FilmPenaltyPrice).HasPrecision(18, 2).IsRequired();
            builder.Property(_ => _.Cost).HasPrecision(18, 2).IsRequired();
            builder.HasOne(_ => _.User)
                           .WithMany(_ => _.Rents)
                           .HasForeignKey(_ => _.UserId);
            builder.HasOne(_ => _.Film)
                           .WithMany(_ => _.Rents)
                           .HasForeignKey(_ => _.FilmId);



        }
    }
}
