using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Migrations
{
    [Migration(202403031526)]
    public class _202403031526_AddGenreTBL : Migration
    {
        public override void Up()
        {
            Create.Table("Genres")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Rate").AsDecimal().NotNullable();

        }
        public override void Down()
        {
            Delete.Table("Genres");
        }

       
    }
}
