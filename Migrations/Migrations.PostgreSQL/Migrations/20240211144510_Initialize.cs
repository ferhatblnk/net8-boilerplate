using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Department");

            migrationBuilder.EnsureSchema(
                name: "Localization");

            migrationBuilder.EnsureSchema(
                name: "Log");

            migrationBuilder.EnsureSchema(
                name: "Membership");

            migrationBuilder.CreateTable(
                name: "TDepartment",
                schema: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Budget = table.Column<decimal>(type: "numeric", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDepartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TLanguage",
                schema: "Localization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LanguageCode = table.Column<string>(type: "text", nullable: true),
                    FlagUrl = table.Column<string>(type: "text", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLanguage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TRole",
                schema: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TSystemLog",
                schema: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Endpoint = table.Column<string>(type: "text", nullable: true),
                    Request = table.Column<string>(type: "text", nullable: true),
                    Response = table.Column<string>(type: "text", nullable: true),
                    Detail = table.Column<string>(type: "text", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    LogTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    LogUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSystemLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUser",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    TokenExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TLocalizedMap",
                schema: "Localization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupCode = table.Column<Guid>(type: "uuid", nullable: true),
                    MapKey = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    LanguageId1 = table.Column<int>(type: "integer", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLocalizedMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TLocalizedMap_TLanguage_LanguageId1",
                        column: x => x.LanguageId1,
                        principalSchema: "Localization",
                        principalTable: "TLanguage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TLocalizedProperty",
                schema: "Localization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
                    TableField = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    LanguageId1 = table.Column<int>(type: "integer", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLocalizedProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TLocalizedProperty_TLanguage_LanguageId1",
                        column: x => x.LanguageId1,
                        principalSchema: "Localization",
                        principalTable: "TLanguage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TUserAddress",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TUserAddress_TUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "TUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TUserDepartment",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TUserDepartment_TDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Department",
                        principalTable: "TDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TUserDepartment_TUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "TUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TUserRole",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TUserRole_TRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Department",
                        principalTable: "TRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TUserRole_TUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "TUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TDepartment_Deleted",
                schema: "Department",
                table: "TDepartment",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TLanguage_Deleted",
                schema: "Localization",
                table: "TLanguage",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TLanguage_RowGuid",
                schema: "Localization",
                table: "TLanguage",
                column: "RowGuid");

            migrationBuilder.CreateIndex(
                name: "IX_TLocalizedMap_Deleted",
                schema: "Localization",
                table: "TLocalizedMap",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TLocalizedMap_GroupCode",
                schema: "Localization",
                table: "TLocalizedMap",
                column: "GroupCode");

            migrationBuilder.CreateIndex(
                name: "IX_TLocalizedMap_LanguageId",
                schema: "Localization",
                table: "TLocalizedMap",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TLocalizedMap_LanguageId1",
                schema: "Localization",
                table: "TLocalizedMap",
                column: "LanguageId1");

            migrationBuilder.CreateIndex(
                name: "IX_TLocalizedProperty_Deleted",
                schema: "Localization",
                table: "TLocalizedProperty",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TLocalizedProperty_LanguageId",
                schema: "Localization",
                table: "TLocalizedProperty",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TLocalizedProperty_LanguageId1",
                schema: "Localization",
                table: "TLocalizedProperty",
                column: "LanguageId1");

            migrationBuilder.CreateIndex(
                name: "IX_TRole_Deleted",
                schema: "Department",
                table: "TRole",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TSystemLog_Deleted",
                schema: "Log",
                table: "TSystemLog",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TSystemLog_GroupId",
                schema: "Log",
                table: "TSystemLog",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TSystemLog_LogUserId",
                schema: "Log",
                table: "TSystemLog",
                column: "LogUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TUser_Deleted",
                schema: "Membership",
                table: "TUser",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TUserAddress_Deleted",
                schema: "Membership",
                table: "TUserAddress",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TUserAddress_UserId",
                schema: "Membership",
                table: "TUserAddress",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TUserDepartment_Deleted",
                schema: "Membership",
                table: "TUserDepartment",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TUserDepartment_DepartmentId",
                schema: "Membership",
                table: "TUserDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TUserDepartment_UserId",
                schema: "Membership",
                table: "TUserDepartment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TUserRole_Deleted",
                schema: "Membership",
                table: "TUserRole",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_TUserRole_RoleId",
                schema: "Membership",
                table: "TUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TUserRole_UserId",
                schema: "Membership",
                table: "TUserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TLocalizedMap",
                schema: "Localization");

            migrationBuilder.DropTable(
                name: "TLocalizedProperty",
                schema: "Localization");

            migrationBuilder.DropTable(
                name: "TSystemLog",
                schema: "Log");

            migrationBuilder.DropTable(
                name: "TUserAddress",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "TUserDepartment",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "TUserRole",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "TLanguage",
                schema: "Localization");

            migrationBuilder.DropTable(
                name: "TDepartment",
                schema: "Department");

            migrationBuilder.DropTable(
                name: "TRole",
                schema: "Department");

            migrationBuilder.DropTable(
                name: "TUser",
                schema: "Membership");
        }
    }
}
