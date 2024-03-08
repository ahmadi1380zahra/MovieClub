using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Migrations
{
    [Migration(202403082158)]
    public class _202403082158_AddRentTbl : Migration
    {
        public override void Up()
        {
            Create.Table("Rents")
                           .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                           .WithColumn("RentAt").AsDate().NotNullable()
                           .WithColumn("GiveBackAt").AsDate().Nullable()
                           .WithColumn("FilmRate").AsDecimal(18, 2).NotNullable()
                           .WithColumn("FilmDailyPrice").AsDecimal(18, 2).NotNullable()
                           .WithColumn("FilmPenaltyPrice").AsDecimal(18, 2).NotNullable()
                           .WithColumn("Cost").AsDecimal(18, 2).NotNullable()
                           .WithColumn("UserId").AsInt32().NotNullable()
                                 .ForeignKey("FK_Rents_Users", "Users", "Id")
                           .WithColumn("FilmId").AsInt32().NotNullable()
                                  .ForeignKey("FK_Rents_Films", "Films", "Id");
        }

        public override void Down()
        {
            Delete.Table("Rents");
        }


    }
}
