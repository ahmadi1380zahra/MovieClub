using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Persistence.EF.Users
{
    public class UserEntityMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_=>_.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(_=>_.LastName).HasMaxLength(50).IsRequired();
            builder.Property(_=>_.Address).IsRequired();
            builder.Property(_=>_.PhoneNumber).HasMaxLength(11).IsRequired();
            builder.Property(_=>_.CreateAt).IsRequired();
            builder.Property(_=>_.Age).IsRequired();
            builder.Property(_=>_.Gender).IsRequired();

        }
    }
}
