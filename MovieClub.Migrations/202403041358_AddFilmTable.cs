﻿using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Migrations
{
    [Migration(202403041358)]
    public class _202403041358_AddFilmTable : Migration
    {
        public override void Up()
        {
            Create.Table("Films")
                 .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                 .WithColumn("Name").AsString(50).NotNullable()
                 .WithColumn("Description").AsString().Nullable()
                 .WithColumn("Director").AsString(50).NotNullable()
                 .WithColumn("Stock").AsInt32().NotNullable()
                 .WithColumn("MinAgeLimit").AsInt32().NotNullable()
                 .WithColumn("PublishYear").AsInt32().NotNullable()
                 .WithColumn("DailyPriceRent").AsDecimal(18, 2).NotNullable()
                 .WithColumn("PenaltyPriceRent").AsDecimal(18, 2).NotNullable()
                 .WithColumn("Duration").AsInt32().NotNullable()
                 .WithColumn("GenreId").AsInt32().NotNullable()
                     .ForeignKey("FK_Films_Genres", "Genres", "Id");
        }
        public override void Down()
        {
            Delete.Table("Films");
        }


    }
}
