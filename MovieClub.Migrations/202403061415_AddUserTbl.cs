using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Migrations
{
    [Migration(202403061415)]
    public class _202403061415_AddUserTbl : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("CreateAt").AsDate().NotNullable()
                .WithColumn("Age").AsDate().NotNullable()
                .WithColumn("Gender").AsInt32().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("PhoneNumber").AsString(11).NotNullable();
             
        }
        public override void Down()
        {
            Delete.Table("Users");
        }


    }
}
