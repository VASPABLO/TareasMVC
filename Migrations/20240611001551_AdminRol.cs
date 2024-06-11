using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT AspNetRoles (Id, [Name], [NormalizedName])
            VALUES ('6b09831a-a689-409b-9384-5b1a4668be54', 'admin', 'ADMIN')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles where Id = '6b09831a-a689-409b-9384-5b1a4668be54'");
        }
    }
}
