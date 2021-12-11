using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTuyenSinh.Data.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: true),
                    FirtName = table.Column<string>(maxLength: 200, nullable: true),
                    Adress = table.Column<string>(maxLength: 200, nullable: true),
                    Telephone = table.Column<string>(fixedLength: true, maxLength: 11, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    BrithDay = table.Column<DateTime>(nullable: true),
                    Position = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admisstion",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    Statust = table.Column<int>(nullable: true),
                    Delete = table.Column<bool>(nullable: true),
                    OpenTime = table.Column<DateTime>(nullable: true),
                    CloseTime = table.Column<DateTime>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admisstion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Advise",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Telephone = table.Column<string>(fixedLength: true, maxLength: 11, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advise", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    id = table.Column<string>(fixedLength: true, maxLength: 9, nullable: false),
                    Desscription = table.Column<string>(maxLength: 500, nullable: true),
                    Delete = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Catergory",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    Delete = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catergory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    id = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    delete = table.Column<bool>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    imgMajor = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameConscious = table.Column<string>(maxLength: 500, nullable: true),
                    idConscious = table.Column<string>(maxLength: 12, nullable: true),
                    NameDistrict = table.Column<string>(maxLength: 500, nullable: true),
                    idDistrict = table.Column<string>(fixedLength: true, maxLength: 12, nullable: true),
                    idShool = table.Column<string>(fixedLength: true, maxLength: 12, nullable: true),
                    NameShool = table.Column<string>(maxLength: 500, nullable: true),
                    Adrees = table.Column<string>(maxLength: 500, nullable: true),
                    Area = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Silde",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(fixedLength: true, nullable: true, defaultValue: new DateTime(2021, 12, 5, 19, 43, 29, 228, DateTimeKind.Local).AddTicks(196)),
                    delete = table.Column<bool>(fixedLength: true, nullable: true),
                    Status = table.Column<bool>(fixedLength: true, nullable: true),
                    OpenTime = table.Column<DateTime>(fixedLength: true, nullable: true),
                    CloseTime = table.Column<DateTime>(fixedLength: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Silde", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileStudent",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idAccount = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 70, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    FromTelePhone = table.Column<string>(maxLength: 300, nullable: true),
                    Adress = table.Column<string>(maxLength: 200, nullable: true),
                    Teletephone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    Religion = table.Column<string>(nullable: true),
                    CreateCMND = table.Column<DateTime>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    idAdmisstion = table.Column<long>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    imgavata = table.Column<string>(maxLength: 70, nullable: true),
                    url = table.Column<string>(maxLength: 300, nullable: true),
                    Statust = table.Column<int>(nullable: true),
                    CMND = table.Column<string>(fixedLength: true, maxLength: 12, nullable: true),
                    Sex = table.Column<int>(nullable: true),
                    BirthDay = table.Column<DateTime>(type: "date", nullable: true),
                    FromBirthDay = table.Column<string>(maxLength: 200, nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    DateRange = table.Column<DateTime>(nullable: true),
                    AdressRange = table.Column<string>(maxLength: 100, nullable: true),
                    Areas = table.Column<string>(maxLength: 100, nullable: true),
                    Nation = table.Column<string>(maxLength: 50, nullable: true),
                    Priority_object = table.Column<string>(maxLength: 100, nullable: true),
                    Shoo1 = table.Column<long>(nullable: true),
                    Shoo2 = table.Column<long>(nullable: true),
                    Shoo3 = table.Column<long>(nullable: true),
                    OpenTime = table.Column<DateTime>(nullable: true),
                    CloseTime = table.Column<DateTime>(nullable: true),
                    Updatedate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileStudent", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProfileStudent_Account_idAccount",
                        column: x => x.idAccount,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileStudent_Admisstion_idAdmisstion",
                        column: x => x.idAdmisstion,
                        principalTable: "Admisstion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatergoryDetails",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    Statust = table.Column<bool>(nullable: true),
                    Detete = table.Column<bool>(nullable: true),
                    idCatergory = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatergoryDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_CatergoryDetails_Catergory_idCatergory",
                        column: x => x.idCatergory,
                        principalTable: "Catergory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admisstion_Major",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMajor = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
                    idAdmisstion = table.Column<long>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: true),
                    Delete = table.Column<bool>(nullable: true),
                    OpenTime = table.Column<DateTime>(nullable: true),
                    CloseTime = table.Column<DateTime>(nullable: true),
                    Statust = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admisstion_Major", x => x.id);
                    table.ForeignKey(
                        name: "FK_Admisstion_Major_Admisstion_idAdmisstion",
                        column: x => x.idAdmisstion,
                        principalTable: "Admisstion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admisstion_Major_Major_idMajor",
                        column: x => x.idMajor,
                        principalTable: "Major",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileProfile",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 1, nullable: true),
                    idProfile = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileProfile", x => x.id);
                    table.ForeignKey(
                        name: "FK_FileProfile_ProfileStudent_idProfile",
                        column: x => x.idProfile,
                        principalTable: "ProfileStudent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InforMationProflie",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMajor = table.Column<string>(maxLength: 10, nullable: true),
                    idBlock = table.Column<string>(fixedLength: true, maxLength: 9, nullable: true),
                    idProfile = table.Column<long>(nullable: true),
                    Statust = table.Column<bool>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    subject1 = table.Column<double>(nullable: true),
                    subject2 = table.Column<double>(nullable: true),
                    subject3 = table.Column<double>(nullable: true),
                    subject4 = table.Column<double>(nullable: true),
                    subject5 = table.Column<double>(nullable: true),
                    subject6 = table.Column<double>(nullable: true),
                    subject7 = table.Column<double>(nullable: true),
                    subject8 = table.Column<double>(nullable: true),
                    subject9 = table.Column<double>(nullable: true),
                    STT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InforMationProflie", x => x.id);
                    table.ForeignKey(
                        name: "FK_InforMationProflie_Block_idBlock",
                        column: x => x.idBlock,
                        principalTable: "Block",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InforMationProflie_ProfileStudent_idProfile",
                        column: x => x.idProfile,
                        principalTable: "ProfileStudent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCategory = table.Column<long>(nullable: true),
                    view = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    Category = table.Column<string>(maxLength: 200, nullable: true),
                    TypePost = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.id);
                    table.ForeignKey(
                        name: "FK_Post_CatergoryDetails_idCategory",
                        column: x => x.idCategory,
                        principalTable: "CatergoryDetails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addmisstion_Major_Block",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idAdmisstion = table.Column<long>(nullable: true),
                    idBlock = table.Column<string>(fixedLength: true, maxLength: 9, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addmisstion_Major_Block", x => x.id);
                    table.ForeignKey(
                        name: "FK_Addmisstion_Major_Block_Admisstion_Major_idAdmisstion",
                        column: x => x.idAdmisstion,
                        principalTable: "Admisstion_Major",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addmisstion_Major_Block_Block_idBlock",
                        column: x => x.idBlock,
                        principalTable: "Block",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 1, nullable: true),
                    idPost = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.id);
                    table.ForeignKey(
                        name: "FK_files_Post_idPost",
                        column: x => x.idPost,
                        principalTable: "Post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addmisstion_Major_Block_idAdmisstion",
                table: "Addmisstion_Major_Block",
                column: "idAdmisstion");

            migrationBuilder.CreateIndex(
                name: "IX_Addmisstion_Major_Block_idBlock",
                table: "Addmisstion_Major_Block",
                column: "idBlock");

            migrationBuilder.CreateIndex(
                name: "IX_Admisstion_Major_idAdmisstion",
                table: "Admisstion_Major",
                column: "idAdmisstion");

            migrationBuilder.CreateIndex(
                name: "IX_Admisstion_Major_idMajor",
                table: "Admisstion_Major",
                column: "idMajor");

            migrationBuilder.CreateIndex(
                name: "IX_CatergoryDetails_idCatergory",
                table: "CatergoryDetails",
                column: "idCatergory");

            migrationBuilder.CreateIndex(
                name: "IX_FileProfile_idProfile",
                table: "FileProfile",
                column: "idProfile");

            migrationBuilder.CreateIndex(
                name: "IX_files_idPost",
                table: "files",
                column: "idPost");

            migrationBuilder.CreateIndex(
                name: "IX_InforMationProflie_idBlock",
                table: "InforMationProflie",
                column: "idBlock");

            migrationBuilder.CreateIndex(
                name: "IX_InforMationProflie_idProfile",
                table: "InforMationProflie",
                column: "idProfile");

            migrationBuilder.CreateIndex(
                name: "IX_Post_idCategory",
                table: "Post",
                column: "idCategory");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileStudent_idAccount",
                table: "ProfileStudent",
                column: "idAccount");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileStudent_idAdmisstion",
                table: "ProfileStudent",
                column: "idAdmisstion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addmisstion_Major_Block");

            migrationBuilder.DropTable(
                name: "Advise");

            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "FileProfile");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "InforMationProflie");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "Silde");

            migrationBuilder.DropTable(
                name: "Admisstion_Major");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "ProfileStudent");

            migrationBuilder.DropTable(
                name: "Major");

            migrationBuilder.DropTable(
                name: "CatergoryDetails");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Admisstion");

            migrationBuilder.DropTable(
                name: "Catergory");
        }
    }
}
