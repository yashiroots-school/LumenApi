using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LumenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__MigrationHistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContextKey = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Model = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.__MigrationHistory", x => new { x.MigrationId, x.ContextKey });
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEndDateUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassAndSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherSection = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ClassAndSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    className = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seatingcapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Classrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__3214EC071BFD2C07", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Depertment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ExamTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FQualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FEMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FAnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MQualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MEMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MAnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MPermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentRefId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfBrothers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfSisters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Student_StudentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Siblings = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.FamilyDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeeHeadingGroups",
                columns: table => new
                {
                    FeeHeadingGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeHeadingGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.FeeHeadingGroups", x => x.FeeHeadingGroupId);
                });

            migrationBuilder.CreateTable(
                name: "FeePlans",
                columns: table => new
                {
                    FeePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeePlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeId = table.Column<int>(type: "int", nullable: false),
                    FeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeValue = table.Column<float>(type: "real", nullable: false),
                    Opt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeType_Id = table.Column<int>(type: "int", nullable: false),
                    TransportOpt_Id = table.Column<int>(type: "int", nullable: false),
                    KmDistance_Id = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jan = table.Column<byte>(type: "tinyint", nullable: false),
                    Feb = table.Column<byte>(type: "tinyint", nullable: false),
                    Mar = table.Column<byte>(type: "tinyint", nullable: false),
                    Apr = table.Column<byte>(type: "tinyint", nullable: false),
                    May = table.Column<byte>(type: "tinyint", nullable: false),
                    Jun = table.Column<byte>(type: "tinyint", nullable: false),
                    Jul = table.Column<byte>(type: "tinyint", nullable: false),
                    Aug = table.Column<byte>(type: "tinyint", nullable: false),
                    Sep = table.Column<byte>(type: "tinyint", nullable: false),
                    Oct = table.Column<byte>(type: "tinyint", nullable: false),
                    Nov = table.Column<byte>(type: "tinyint", nullable: false),
                    Dec = table.Column<byte>(type: "tinyint", nullable: false),
                    Fee_configurationid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeConfigurationname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Batch_Id = table.Column<int>(type: "int", nullable: false),
                    Batch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.FeePlans", x => x.FeePlanId);
                });

            migrationBuilder.CreateTable(
                name: "Frequencys",
                columns: table => new
                {
                    FeeFrequencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeFrequencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Frequencys", x => x.FeeFrequencyId);
                });

            migrationBuilder.CreateTable(
                name: "GradingCriterias",
                columns: table => new
                {
                    CriteriaID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinimumPercentage = table.Column<decimal>(type: "decimal(18,1)", nullable: true),
                    MaximumPercentage = table.Column<decimal>(type: "decimal(18,1)", nullable: true),
                    Grade = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    GradeDescription = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    BoardID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradingCriteria", x => x.CriteriaID);
                });

            migrationBuilder.CreateTable(
                name: "LabelControls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LableId = table.Column<int>(type: "int", nullable: false),
                    LabelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    School_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.LabelControls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LableId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubMenu_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.MasterLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.MasterReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MigrationHistory_10032021",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContextKey = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Model = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "MigrationHistory_22032021",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContextKey = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Model = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PeriodSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher15 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.PeriodSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportHeadings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    HeadingId = table.Column<int>(type: "int", nullable: false),
                    OrderNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ReportHeadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePagePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasPermission = table.Column<bool>(type: "bit", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageViewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.RolePagePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolBoards",
                columns: table => new
                {
                    BoardID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolBoards", x => x.BoardID);
                });

            migrationBuilder.CreateTable(
                name: "SMSEMAILSCHEDULEs",
                columns: table => new
                {
                    SMSEMAILSCHEDULEID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SCHEDULETYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATEDDATE = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.SMSEMAILSCHEDULEs", x => x.SMSEMAILSCHEDULEID);
                });

            migrationBuilder.CreateTable(
                name: "SMSEMAILSENDHISTORies",
                columns: table => new
                {
                    HISTORYID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SENDERID = table.Column<int>(type: "int", nullable: false),
                    SENDERTYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SUBJECT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATTACHEDFILE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATTACHEDFILETYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATTACHEDFILENAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATEDDATE = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.SMSEMAILSENDHISTORies", x => x.HISTORYID);
                });

            migrationBuilder.CreateTable(
                name: "SMSEMAILTEMPLETES",
                columns: table => new
                {
                    SMSEMAILID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOTIFICATIONTYPE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SMS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SUBJECT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ATTACHEDFILE = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    ATTACHEDFILETYPE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ATTACHEDFILENAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CREATEDDATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSSubject = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staffid = table.Column<int>(type: "int", nullable: false),
                    staffname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    staffaddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__staff__645AE4A6656C112C", x => x.staffid);
                });

            migrationBuilder.CreateTable(
                name: "StafsDetails",
                columns: table => new
                {
                    StafId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeInWords = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherTongue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BesicSallery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerksSallery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossSallery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastOrganizationofEmployment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoofYearsattheLastAssignment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelievingLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformanceLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormalitiesCheck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherOrHusbandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MothersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MariedStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Children = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BesicSallery1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerksSallery1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossSallery1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofReliving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffSignatureFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankACNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFSC_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_AccountId = table.Column<int>(type: "int", nullable: false),
                    Employee_AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UAN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StafsDetails", x => x.StafId);
                });

            migrationBuilder.CreateTable(
                name: "StudentCategorys",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StudentCategorys", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "StudentLoginDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentL__3214EC076EF57B66", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentLoginHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StudentLoginHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentRegistrationHistories",
                columns: table => new
                {
                    StudentRegisterHistoryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentRegisterID = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeInWords = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherTongue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sports = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkForIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApplyforTC = table.Column<bool>(type: "bit", nullable: false),
                    IsApplyforAdmission = table.Column<bool>(type: "bit", nullable: false),
                    IsApprove = table.Column<int>(type: "int", nullable: false),
                    IsApprovePreview = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsInsertFromAd = table.Column<bool>(type: "bit", nullable: true),
                    IsAdmissionPaid = table.Column<bool>(type: "bit", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Parents_Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StudentRegistrationHistories", x => x.StudentRegisterHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "StudentRegNumberMasters",
                columns: table => new
                {
                    StudnetRegNumberMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegPrefix = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegLength = table.Column<int>(type: "int", nullable: true),
                    RegNumberStartWith = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    RegStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    RegLastNumber = table.Column<int>(type: "int", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Batch_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "StudentResetPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ResetKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StudentResetPasswords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeInWords = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherTongue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sports = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkForIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApplyforTC = table.Column<bool>(type: "bit", nullable: false),
                    IsApplyforAdmission = table.Column<bool>(type: "bit", nullable: false),
                    IsApprove = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsInsertFromAd = table.Column<bool>(type: "bit", nullable: true),
                    IsAdmissionPaid = table.Column<bool>(type: "bit", nullable: true),
                    RegNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Batch_Id = table.Column<int>(type: "int", nullable: false),
                    ParentEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionFeePaid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transport_Options = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup_Id = table.Column<int>(type: "int", nullable: false),
                    isPromoted = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Section_Id = table.Column<int>(type: "int", nullable: true),
                    RollNo = table.Column<long>(type: "bigint", nullable: true),
                    ScholarNo = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "StudentsRegistrations",
                columns: table => new
                {
                    StudentRegisterID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeInWords = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherTongue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sports = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkForIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdharFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApplyforTC = table.Column<bool>(type: "bit", nullable: false),
                    IsApplyforAdmission = table.Column<bool>(type: "bit", nullable: false),
                    IsApprove = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsInsertFromAd = table.Column<bool>(type: "bit", nullable: true),
                    IsAdmissionPaid = table.Column<bool>(type: "bit", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastStudiedSchoolName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parents_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Batch_Id = table.Column<int>(type: "int", nullable: false),
                    Batch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup_Id = table.Column<int>(type: "int", nullable: false),
                    Religion_Id = table.Column<int>(type: "int", nullable: false),
                    Cast_Id = table.Column<int>(type: "int", nullable: false),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transport_Options = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionFeePaid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Registration_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailsent = table.Column<bool>(type: "bit", nullable: false),
                    Promotion_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Promotion_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_SendDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_send = table.Column<int>(type: "int", nullable: false),
                    Grade_Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    House = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hostel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSSMIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRTEStudent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInDayCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilySSSMID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankACHolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankIFSC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subjects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionalSubjects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUserLoggedIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RollNo = table.Column<long>(type: "bigint", nullable: true),
                    ScholarNo = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StudentsRegistrations", x => x.StudentRegisterID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Teacher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Subject_ID = table.Column<int>(type: "int", nullable: false),
                    Batch_Id = table.Column<int>(type: "int", nullable: false),
                    Class_Teacher = table.Column<bool>(type: "bit", nullable: false),
                    Section_Id = table.Column<int>(type: "int", nullable: true),
                    Section = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AcademicDetail",
                columns: table => new
                {
                    AcademicDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewProperty = table.Column<int>(type: "int", nullable: false),
                    ScholarNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Institution = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    University = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Addedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addeby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Spare1 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare2 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare3 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Dateon = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Stream = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_AcademicDetail", x => x.AcademicDetailId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_AccountSummary",
                columns: table => new
                {
                    Summary_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetPay = table.Column<int>(type: "int", nullable: false),
                    PF = table.Column<int>(type: "int", nullable: false),
                    Basic_Salary = table.Column<int>(type: "int", nullable: false),
                    Deduction_Amt = table.Column<int>(type: "int", nullable: false),
                    Arrear_Amt = table.Column<int>(type: "int", nullable: false),
                    Arrear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DA = table.Column<int>(type: "int", nullable: false),
                    Professional_Tax = table.Column<int>(type: "int", nullable: false),
                    Added_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Contribution = table.Column<int>(type: "int", nullable: false),
                    Employer_Contribution = table.Column<int>(type: "int", nullable: false),
                    Net_Pay = table.Column<int>(type: "int", nullable: false),
                    Attendence_Percentage = table.Column<int>(type: "int", nullable: false),
                    ESI = table.Column<int>(type: "int", nullable: false),
                    Gross = table.Column<int>(type: "int", nullable: false),
                    Total_Salary = table.Column<int>(type: "int", nullable: false),
                    LOP = table.Column<double>(type: "float", nullable: false),
                    CCA = table.Column<int>(type: "int", nullable: false),
                    HRA = table.Column<int>(type: "int", nullable: false),
                    OtherALlowance = table.Column<int>(type: "int", nullable: false),
                    NoOfdayspresent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_AccountSummary", x => x.Summary_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_AccountType",
                columns: table => new
                {
                    Account_TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account_Typename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_AccountType", x => x.Account_TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ArchieveChangeStaffAccounttype",
                columns: table => new
                {
                    ChangeAccounType_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StafID = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staf_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_AccountId = table.Column<int>(type: "int", nullable: false),
                    Employee_AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_ArchieveChangeStaffAccounttype", x => x.ChangeAccounType_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ArchieveStaffSalary",
                columns: table => new
                {
                    Archieve_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_ID = table.Column<int>(type: "int", nullable: false),
                    Staff_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary_Amount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Basic_Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_ArchieveStaffSalary", x => x.Archieve_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Arrear",
                columns: table => new
                {
                    Arrear_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Arrear_Amt = table.Column<int>(type: "int", nullable: false),
                    Arrear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Net_Pay = table.Column<int>(type: "int", nullable: false),
                    Added_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deduction_Amt = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Arrear", x => x.Arrear_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Assignment",
                columns: table => new
                {
                    Assignment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject_ID = table.Column<int>(type: "int", nullable: false),
                    New_Assignment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assignment_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Submitted_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Assignment", x => x.Assignment_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_BasicPayDetails",
                columns: table => new
                {
                    BasicAmount_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolCategory_Id = table.Column<int>(type: "int", nullable: false),
                    BasicPay_Id = table.Column<int>(type: "int", nullable: false),
                    Category_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Basicpay_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Basic_Amount = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_BasicPayDetails", x => x.BasicAmount_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_BasicpayMaster",
                columns: table => new
                {
                    BasicPay_MasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Basicpay_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_BasicpayMaster", x => x.BasicPay_MasterId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Batches",
                columns: table => new
                {
                    Batch_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Batch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActiveForAdmission = table.Column<bool>(type: "bit", nullable: false),
                    IsActiveForPayments = table.Column<bool>(type: "bit", nullable: false),
                    IsActiveForRegistrationFee = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Batches", x => x.Batch_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_BloodGroup",
                columns: table => new
                {
                    BloodGroup_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Blood_Group = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_BloodGroup", x => x.BloodGroup_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Caste",
                columns: table => new
                {
                    Caste_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Caste_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Caste", x => x.Caste_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Category",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Category", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Class",
                columns: table => new
                {
                    Class_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Class", x => x.Class_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Classsetup",
                columns: table => new
                {
                    Class_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Classsetup", x => x.Class_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ClassSubject",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassID = table.Column<long>(type: "bigint", nullable: true),
                    SubjectID = table.Column<long>(type: "bigint", nullable: true),
                    BoardID = table.Column<long>(type: "bigint", nullable: true),
                    IsElective = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ClassSubject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CommonDataListItem",
                columns: table => new
                {
                    DatalistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataListName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataListItemName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Spare1 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare2 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare3 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_CommonDataListItem", x => x.DatalistId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CoScholastic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    BoardID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_CoSc__3214EC0745BE5BA9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CoScholastic_Result",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardID = table.Column<long>(type: "bigint", nullable: true),
                    ObtainedGrade = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    StudentID = table.Column<long>(type: "bigint", nullable: true),
                    CoScholasticID = table.Column<long>(type: "bigint", nullable: true),
                    ClassID = table.Column<long>(type: "bigint", nullable: true),
                    TermID = table.Column<long>(type: "bigint", nullable: true),
                    SectionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_CoScholastic_Result", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CoScholasticClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardID = table.Column<long>(type: "bigint", nullable: true),
                    ClassID = table.Column<long>(type: "bigint", nullable: true),
                    CoscholasticID = table.Column<long>(type: "bigint", nullable: true),
                    ClassName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_CoScholasticClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CoScholasticObtainedGrade",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObtainedCoScholasticID = table.Column<long>(type: "bigint", nullable: true),
                    ObtainedGrade = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    CoscholasticID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CoScholasticObtainedGrade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CreateBank",
                columns: table => new
                {
                    Bank_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bank_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandlineNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contactperson_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_CreateBank", x => x.Bank_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CreateBranch",
                columns: table => new
                {
                    Branch_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bank_Id = table.Column<int>(type: "int", nullable: false),
                    Bank_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Landline_No = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_CreateBranch", x => x.Branch_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_CreateMerchantId",
                columns: table => new
                {
                    Merchant_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    School_Id = table.Column<int>(type: "int", nullable: false),
                    Bank_Id = table.Column<int>(type: "int", nullable: false),
                    Branch_Id = table.Column<int>(type: "int", nullable: false),
                    MerchantName_Id = table.Column<int>(type: "int", nullable: false),
                    MerchantMID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_CreateMerchantId", x => x.Merchant_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DataList",
                columns: table => new
                {
                    DataListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataListName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_DataList", x => x.DataListId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DataListItem",
                columns: table => new
                {
                    DataListItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataListItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataListId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataListName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_DataListItem", x => x.DataListItemId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Deductions",
                columns: table => new
                {
                    Deductions_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Net_Pay = table.Column<int>(type: "int", nullable: false),
                    Deduction_Amt = table.Column<int>(type: "int", nullable: false),
                    Added_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Deductions", x => x.Deductions_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Department",
                columns: table => new
                {
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_EPFStatement",
                columns: table => new
                {
                    EPFstatement_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gross_Wages = table.Column<int>(type: "int", nullable: false),
                    Epf_Wages = table.Column<int>(type: "int", nullable: false),
                    EPs_Wages = table.Column<int>(type: "int", nullable: false),
                    EDLIWages = table.Column<int>(type: "int", nullable: false),
                    Employe_Contribution = table.Column<int>(type: "int", nullable: false),
                    Employer_Contribution = table.Column<int>(type: "int", nullable: false),
                    EPS_Pension = table.Column<int>(type: "int", nullable: false),
                    NCP_Days = table.Column<int>(type: "int", nullable: false),
                    Refund_Advances = table.Column<int>(type: "int", nullable: false),
                    Added_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffCategory_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_EPFStatement", x => x.EPFstatement_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ExamTypes",
                columns: table => new
                {
                    Exam_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exam_Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_ExamTypes", x => x.Exam_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_MenuName",
                columns: table => new
                {
                    Menu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Menu_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Upload_Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_MenuName", x => x.Menu_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_MerchantName",
                columns: table => new
                {
                    MerchantName_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    School_Id = table.Column<int>(type: "int", nullable: false),
                    Bank_Id = table.Column<int>(type: "int", nullable: false),
                    Branch_Id = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_MerchantName", x => x.MerchantName_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PaymentTransactionDetails",
                columns: table => new
                {
                    PaymentTransactionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionStatus = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TransactionError = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TxnDate = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TrackId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pmntmode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Card = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Member = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_PaymentTransactionDetails", x => x.PaymentTransactionId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PaymentTransactionFeeDetails",
                columns: table => new
                {
                    PaymentFeedetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTransactionId = table.Column<int>(type: "int", nullable: true),
                    FeeID = table.Column<int>(type: "int", nullable: true),
                    FeeAmount = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Portions",
                columns: table => new
                {
                    Portion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject_ID = table.Column<int>(type: "int", nullable: false),
                    Portion_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Portions", x => x.Portion_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Religion",
                columns: table => new
                {
                    Religion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Religion_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Religion", x => x.Religion_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Remark",
                columns: table => new
                {
                    RemarkId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Remark = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    TermId = table.Column<long>(type: "bigint", nullable: true),
                    BoardId = table.Column<long>(type: "bigint", nullable: true),
                    StudentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Remark", x => x.RemarkId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Revision",
                columns: table => new
                {
                    Revision_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject_ID = table.Column<int>(type: "int", nullable: false),
                    Revision_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Revision", x => x.Revision_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_RolePermissionNew",
                columns: table => new
                {
                    Rolepermission_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Menu_Id = table.Column<int>(type: "int", nullable: false),
                    Submenu_Id = table.Column<int>(type: "int", nullable: false),
                    Submenu_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Create_permission = table.Column<bool>(type: "bit", nullable: false),
                    Edit_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Update_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Delete_Permission = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Submenu_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Submenu_permission = table.Column<bool>(type: "bit", nullable: false),
                    Staff_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_RolePermissionNew", x => x.Rolepermission_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Room",
                columns: table => new
                {
                    Room_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Room_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Room_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seating_Capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomType_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Room", x => x.Room_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_RoomType",
                columns: table => new
                {
                    Room_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_RoomType", x => x.Room_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SalaryStatement",
                columns: table => new
                {
                    SalaryStatement_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employers_Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Code = table.Column<int>(type: "int", nullable: false),
                    Employee_AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountDetails_Id = table.Column<int>(type: "int", nullable: false),
                    Account_Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salarystatement_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salarystatement_year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalaryStatement_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffCategory_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_SalaryStatement", x => x.SalaryStatement_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SchoolSetup",
                columns: table => new
                {
                    Schoolsetup_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    School_Id = table.Column<int>(type: "int", nullable: false),
                    Bank_Id = table.Column<int>(type: "int", nullable: false),
                    Branch_Id = table.Column<int>(type: "int", nullable: false),
                    Merchant_nameId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee_configurationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee_Configuratinname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_SchoolSetup", x => x.Schoolsetup_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SectionSetup",
                columns: table => new
                {
                    Section_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_SectionSetup", x => x.Section_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Semester",
                columns: table => new
                {
                    SemesterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Sem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Percentage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Addedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addeby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Spare1 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare2 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare3 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    perse2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Persentagegrade = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_Semester", x => x.SemesterId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SetTime",
                columns: table => new
                {
                    Time_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_SetTime", x => x.Time_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Siblings",
                columns: table => new
                {
                    Siblings_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Id = table.Column<int>(type: "int", nullable: false),
                    Studentname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_id = table.Column<int>(type: "int", nullable: false),
                    Confirmation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyDetails_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_Siblings", x => x.Siblings_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_skillset",
                columns: table => new
                {
                    SkillsetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Problemsolving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Initiative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adaptabilitytochange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interpersonalskills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strategicthinking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timemanagement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Communication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Leadership = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teamwork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dancing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Singing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contentwriting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoralDraw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photoshop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drawing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spare1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spare2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spare3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_skillset", x => x.SkillsetId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_StaffAttendance",
                columns: table => new
                {
                    StaffAtte_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark_FullDayAbsent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark_HalfDayAbsent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attendence_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attendence_Day = table.Column<int>(type: "int", nullable: false),
                    Attendence_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attendence_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark_FullDayPresent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_StaffAttendance", x => x.StaffAtte_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_StaffCategory",
                columns: table => new
                {
                    Staff_Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_StaffCategory", x => x.Staff_Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_StaffSalary",
                columns: table => new
                {
                    Salary_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_ID = table.Column<int>(type: "int", nullable: false),
                    Staff_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary_Amount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Basic_Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_StaffSalary", x => x.Salary_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Student_ElectiveRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: true),
                    ElectiveSubjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Student_ElectiveRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_StudentAttendance",
                columns: table => new
                {
                    Attendance_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark_FullDayAbsent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark_HalfDayAbsent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentRegisterID = table.Column<int>(type: "int", nullable: false),
                    Student_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_StudentAttendance", x => x.Attendance_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StudentDetail",
                columns: table => new
                {
                    ScholarNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Course = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Years = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Sibiling1 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Sibiling2 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Sibiling3 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Sibiling4 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Sibiling5 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FacultyMentor = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DateofBirth = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Age = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CorrespondenceAddress = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ResidenceLocation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OutStationStudent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NativePlace = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Hostalite = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FatherProfession = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FatherCountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FatherMobileNo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FatherEmailId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FatherCompanyName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MotherProfession = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MotherCountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MotherMobileNo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MotherEmailId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MotherCompanyName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addeby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Spare1 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare2 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare3 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    CMCRemarks = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DateOn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Class = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Religious = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReligiousOther = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Batch_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_StudentDetail", x => x.ScholarNumber);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_StudentPromote",
                columns: table => new
                {
                    PromoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromClass_Id = table.Column<int>(type: "int", nullable: false),
                    ToClass_Id = table.Column<int>(type: "int", nullable: false),
                    Student_Id = table.Column<int>(type: "int", nullable: false),
                    Registration_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Batch_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Id = table.Column<int>(type: "int", nullable: true),
                    ToSection = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_StudentPromote", x => x.PromoteId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SubjectsSetup",
                columns: table => new
                {
                    Subject_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsElective = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_SubjectsSetup", x => x.Subject_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SubmenuName",
                columns: table => new
                {
                    Submenu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Submenu_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Menu_Id = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Submenu_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Submenu_permission = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_SubmenuName", x => x.Submenu_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TcAmount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_TcAmount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TeacherAllocation",
                columns: table => new
                {
                    Allocate_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Teacher_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Subject_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_TeacherAllocation", x => x.Allocate_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Term",
                columns: table => new
                {
                    TermID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TermName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    UpdatedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    BoardID = table.Column<long>(type: "bigint", nullable: true),
                    BatchId = table.Column<long>(type: "bigint", nullable: true),
                    ClassId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Term", x => x.TermID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TestObtainedMark",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordIDFK = table.Column<long>(type: "bigint", nullable: true),
                    ObtainedMarks = table.Column<decimal>(type: "decimal(18,1)", nullable: true),
                    TestID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_TestObtainedMark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TestRecords",
                columns: table => new
                {
                    RecordID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<long>(type: "bigint", nullable: true),
                    TestID = table.Column<long>(type: "bigint", nullable: true),
                    TermID = table.Column<long>(type: "bigint", nullable: true),
                    ClassID = table.Column<long>(type: "bigint", nullable: true),
                    SectionID = table.Column<long>(type: "bigint", nullable: true),
                    ObtainedMarks = table.Column<decimal>(type: "numeric(18,1)", nullable: true),
                    BoardID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_TestRecords", x => x.RecordID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Tests",
                columns: table => new
                {
                    TestID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassID = table.Column<long>(type: "bigint", nullable: true),
                    SubjectID = table.Column<long>(type: "bigint", nullable: true),
                    TestName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    TestType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MaximumMarks = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    TermID = table.Column<long>(type: "bigint", nullable: true),
                    BoardID = table.Column<long>(type: "bigint", nullable: true),
                    IsOptional = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Tests", x => x.TestID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TimeTable",
                columns: table => new
                {
                    TimeTable_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Staff_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StafId = table.Column<int>(type: "int", nullable: false),
                    Room_Id = table.Column<int>(type: "int", nullable: false),
                    Room_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Day_Time_Id = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject_ID = table.Column<int>(type: "int", nullable: false),
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Day_ID = table.Column<int>(type: "int", nullable: false),
                    Time_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_TimeTable", x => x.TimeTable_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TransportKm",
                columns: table => new
                {
                    Km_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From_Km = table.Column<float>(type: "real", nullable: false),
                    To_Km = table.Column<float>(type: "real", nullable: false),
                    Batch_Id = table.Column<int>(type: "int", nullable: false),
                    School_Id = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AMount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_TransportKm", x => x.Km_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_UserManagement",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_UserManagement", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_WeekDays",
                columns: table => new
                {
                    Day_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Week_day = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Tbl_WeekDays", x => x.Day_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WorkExperience",
                columns: table => new
                {
                    WorkExperienceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalExperience = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CompanyProfile = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FromDate = table.Column<int>(type: "int", nullable: false),
                    ToDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addeby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Spare1 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare2 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare3 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_WorkExperience", x => x.WorkExperienceId);
                });

            migrationBuilder.CreateTable(
                name: "TblCreateSchools",
                columns: table => new
                {
                    School_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    School_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Copyright = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Upload_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblCreateSchools", x => x.School_Id);
                });

            migrationBuilder.CreateTable(
                name: "TblDueFees",
                columns: table => new
                {
                    DueFeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeHeadingId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    May = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Oct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidMonths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayHeadings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFee = table.Column<float>(type: "real", nullable: false),
                    FeePaids = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseSpecialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeReceiptsOneTimeCreator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeHeading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidAmount = table.Column<float>(type: "real", nullable: false),
                    DueAmount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblDueFees", x => x.DueFeeId);
                });

            migrationBuilder.CreateTable(
                name: "TblEmailArchieves",
                columns: table => new
                {
                    Email_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_id = table.Column<int>(type: "int", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parent_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblEmailArchieves", x => x.Email_Id);
                });

            migrationBuilder.CreateTable(
                name: "TblFeeReceipts",
                columns: table => new
                {
                    FeeReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    Jan = table.Column<bool>(type: "bit", nullable: false),
                    Feb = table.Column<bool>(type: "bit", nullable: false),
                    Mar = table.Column<bool>(type: "bit", nullable: false),
                    Apr = table.Column<bool>(type: "bit", nullable: false),
                    May = table.Column<bool>(type: "bit", nullable: false),
                    Jun = table.Column<bool>(type: "bit", nullable: false),
                    Jul = table.Column<bool>(type: "bit", nullable: false),
                    Aug = table.Column<bool>(type: "bit", nullable: false),
                    Sep = table.Column<bool>(type: "bit", nullable: false),
                    Oct = table.Column<bool>(type: "bit", nullable: false),
                    Nov = table.Column<bool>(type: "bit", nullable: false),
                    Dec = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidMonths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Concession = table.Column<float>(type: "real", nullable: false),
                    ConcessionAmt = table.Column<float>(type: "real", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayHeadings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldBalance = table.Column<float>(type: "real", nullable: false),
                    ReceiptAmt = table.Column<float>(type: "real", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFee = table.Column<float>(type: "real", nullable: false),
                    LateFee = table.Column<float>(type: "real", nullable: false),
                    BalanceAmt = table.Column<float>(type: "real", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeePaids = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeReceiptsOneTimeCreator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueAmount = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    PaidAmount = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeHeadingIDs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeconfigurationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feeconfigurationname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblFeeReceipts", x => x.FeeReceiptId);
                });

            migrationBuilder.CreateTable(
                name: "TblLateFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    FeeHeadingId = table.Column<int>(type: "int", nullable: false),
                    LateFee = table.Column<float>(type: "real", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblLateFees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblStudentFeeSaveds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TotalFee = table.Column<float>(type: "real", nullable: false),
                    FeePaid = table.Column<float>(type: "real", nullable: false),
                    OldFee = table.Column<float>(type: "real", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblStudentFeeSaveds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblTestAssignDate",
                columns: table => new
                {
                    TestAssignID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestID = table.Column<long>(type: "bigint", nullable: true),
                    BoardID = table.Column<long>(type: "bigint", nullable: true),
                    BatchId = table.Column<long>(type: "bigint", nullable: true),
                    ClassId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTestAssignDate", x => x.TestAssignID);
                });

            migrationBuilder.CreateTable(
                name: "TblTransportFeeReceipts",
                columns: table => new
                {
                    FeeReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeHeadingId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Jan = table.Column<bool>(type: "bit", nullable: false),
                    Feb = table.Column<bool>(type: "bit", nullable: false),
                    Mar = table.Column<bool>(type: "bit", nullable: false),
                    Apr = table.Column<bool>(type: "bit", nullable: false),
                    May = table.Column<bool>(type: "bit", nullable: false),
                    Jun = table.Column<bool>(type: "bit", nullable: false),
                    Jul = table.Column<bool>(type: "bit", nullable: false),
                    Aug = table.Column<bool>(type: "bit", nullable: false),
                    Sep = table.Column<bool>(type: "bit", nullable: false),
                    Oct = table.Column<bool>(type: "bit", nullable: false),
                    Nov = table.Column<bool>(type: "bit", nullable: false),
                    Dec = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidMonths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Concession = table.Column<float>(type: "real", nullable: false),
                    ConcessionAmt = table.Column<float>(type: "real", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayHeadings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldBalance = table.Column<float>(type: "real", nullable: false),
                    ReceiptAmt = table.Column<float>(type: "real", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFee = table.Column<float>(type: "real", nullable: false),
                    LateFee = table.Column<float>(type: "real", nullable: false),
                    BalanceAmt = table.Column<float>(type: "real", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeePaids = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeReceiptsOneTimeCreator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueAmount = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    PaidAmount = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblTransportFeeReceipts", x => x.FeeReceiptId);
                });

            migrationBuilder.CreateTable(
                name: "TblTransportReducedAmounts",
                columns: table => new
                {
                    ReducedAmount_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Range = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblTransportReducedAmounts", x => x.ReducedAmount_Id);
                });

            migrationBuilder.CreateTable(
                name: "TblUserDynamicConfigurations",
                columns: table => new
                {
                    Mainid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TblUserDynamicConfigurations", x => x.Mainid);
                });

            migrationBuilder.CreateTable(
                name: "TermClassMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TermId = table.Column<long>(type: "bigint", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TermClas__3214EC072A6B46EF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time22 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time33 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time44 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time55 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time66 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time77 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time88 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time99 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time101 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time102 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TimeSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportFeeConfigurations",
                columns: table => new
                {
                    TransportFeeConfigurationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FromKM = table.Column<int>(type: "int", nullable: true),
                    ToKM = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Batch_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TransportFeeHeadings",
                columns: table => new
                {
                    TransportFeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeFrequencyId = table.Column<int>(type: "int", nullable: false),
                    FeeFrequencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jan = table.Column<byte>(type: "tinyint", nullable: false),
                    Mar = table.Column<byte>(type: "tinyint", nullable: false),
                    Apr = table.Column<byte>(type: "tinyint", nullable: false),
                    May = table.Column<byte>(type: "tinyint", nullable: false),
                    Jun = table.Column<byte>(type: "tinyint", nullable: false),
                    Jul = table.Column<byte>(type: "tinyint", nullable: false),
                    Aug = table.Column<byte>(type: "tinyint", nullable: false),
                    Sep = table.Column<byte>(type: "tinyint", nullable: false),
                    Oct = table.Column<byte>(type: "tinyint", nullable: false),
                    Nov = table.Column<byte>(type: "tinyint", nullable: false),
                    Dec = table.Column<byte>(type: "tinyint", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Feb = table.Column<byte>(type: "tinyint", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TransportFeeHeadings", x => x.TransportFeeId);
                });

            migrationBuilder.CreateTable(
                name: "TransportFeePlans",
                columns: table => new
                {
                    FeePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeePlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeId = table.Column<int>(type: "int", nullable: false),
                    FeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeValue = table.Column<float>(type: "real", nullable: false),
                    Opt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opt4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TransportFeePlans", x => x.FeePlanId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey, x.UserId });
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeeHeadings",
                columns: table => new
                {
                    FeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeFrequencyId = table.Column<int>(type: "int", nullable: false),
                    FeeFrequencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jan = table.Column<byte>(type: "tinyint", nullable: false),
                    Mar = table.Column<byte>(type: "tinyint", nullable: false),
                    Apr = table.Column<byte>(type: "tinyint", nullable: false),
                    May = table.Column<byte>(type: "tinyint", nullable: false),
                    Jun = table.Column<byte>(type: "tinyint", nullable: false),
                    Jul = table.Column<byte>(type: "tinyint", nullable: false),
                    Aug = table.Column<byte>(type: "tinyint", nullable: false),
                    Sep = table.Column<byte>(type: "tinyint", nullable: false),
                    Oct = table.Column<byte>(type: "tinyint", nullable: false),
                    Nov = table.Column<byte>(type: "tinyint", nullable: false),
                    Dec = table.Column<byte>(type: "tinyint", nullable: false),
                    FeeHeadingGroups_FeeHeadingGroupId = table.Column<int>(type: "int", nullable: true),
                    Accounts_AccountId = table.Column<int>(type: "int", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Feb = table.Column<byte>(type: "tinyint", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeType_Id = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.FeeHeadings", x => x.FeeId);
                    table.ForeignKey(
                        name: "FK_dbo.FeeHeadings_dbo.Accounts_Accounts_AccountId",
                        column: x => x.Accounts_AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_dbo.FeeHeadings_dbo.FeeHeadingGroups_FeeHeadingGroups_FeeHeadingGroupId",
                        column: x => x.FeeHeadingGroups_FeeHeadingGroupId,
                        principalTable: "FeeHeadingGroups",
                        principalColumn: "FeeHeadingGroupId");
                    table.ForeignKey(
                        name: "FK_dbo.FeeHeadings_dbo.Frequencys_FeeFrequencyId",
                        column: x => x.FeeFrequencyId,
                        principalTable: "Frequencys",
                        principalColumn: "FeeFrequencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignSection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeStructureApplicable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistancefromSchool = table.Column<float>(type: "real", nullable: false),
                    TransportFacility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthCertificateAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThreePassportSizePhotographs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MigrationCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentRefId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: false),
                    Class_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section_Id = table.Column<int>(type: "int", nullable: false),
                    Section_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Student_StudentId = table.Column<int>(type: "int", nullable: true),
                    TransportOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physicallychalanged = table.Column<bool>(type: "bit", nullable: false),
                    IncomeCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherAdhar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherAdhar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ssmid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportVehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AdditionalInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.AdditionalInformations_dbo.Students_Student_StudentId",
                        column: x => x.Student_StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "GuardianDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuardianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentRefId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Student_StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.GuardianDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.GuardianDetails_dbo.Students_Student_StudentId",
                        column: x => x.Student_StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "PastSchoolingReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfSchoolLastAttended = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassPassed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonForLeaving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarksCardAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharacterConductCertificateAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentRefId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Promotion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Student_StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.PastSchoolingReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.PastSchoolingReports_dbo.Students_Student_StudentId",
                        column: x => x.Student_StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "StudentRemoteAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnterDesiredlogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentRefId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StudentRemoteAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.StudentRemoteAccesses_dbo.Students_StudentRefId",
                        column: x => x.StudentRefId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTcDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ispaid = table.Column<bool>(type: "bit", nullable: false),
                    TcId = table.Column<long>(type: "bigint", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    ExamStatusId = table.Column<int>(type: "int", nullable: false),
                    PromoteClassId = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemarksID = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonID = table.Column<int>(type: "int", nullable: true),
                    SchoolLeftDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    PromoteSectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StudentTcDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.StudentTcDetails_dbo.Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Declaration",
                columns: table => new
                {
                    DeclarationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Interesterd = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NotInterested = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Relocate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StudentName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Agree = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Addedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addeby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Spare1 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare2 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare3 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_Declaration", x => x.DeclarationId);
                    table.ForeignKey(
                        name: "FK_dbo.tbl_Declaration_dbo.tbl_StudentDetail_ScholarNumber",
                        column: x => x.ScholarNumber,
                        principalTable: "tbl_StudentDetail",
                        principalColumn: "ScholarNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SummerInternship",
                columns: table => new
                {
                    SummerInternshipId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectTitle = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FacultyProjectGuide = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FacultyGuideMobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IndustryGuideName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IndustryGuideDesignation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IndustryGuideTelNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IndustryGuideMobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IndustryGuideEmail = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    StipendinThousands = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ProjectSubmission = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ReasonforNoSubmission = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    PrePlacementOfferReceived = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Addedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Addeby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updatedby = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Spare1 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare2 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Spare3 = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.tbl_SummerInternship", x => x.SummerInternshipId);
                    table.ForeignKey(
                        name: "FK_dbo.tbl_SummerInternship_dbo.tbl_StudentDetail_ScholarNumber",
                        column: x => x.ScholarNumber,
                        principalTable: "tbl_StudentDetail",
                        principalColumn: "ScholarNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TcFeeDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentTcDetailsId = table.Column<long>(type: "bigint", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ReceiptAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsTcfee = table.Column<bool>(type: "bit", nullable: true),
                    PaidDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TcFeeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.TcFeeDetails_dbo.StudentTcDetails_StudentTcDetailsId",
                        column: x => x.StudentTcDetailsId,
                        principalTable: "StudentTcDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_dbo.TcFeeDetails_dbo.Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInformations_Student_StudentId",
                table: "AdditionalInformations",
                column: "Student_StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeHeadings_Accounts_AccountId",
                table: "FeeHeadings",
                column: "Accounts_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeHeadings_FeeFrequencyId",
                table: "FeeHeadings",
                column: "FeeFrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeHeadings_FeeHeadingGroups_FeeHeadingGroupId",
                table: "FeeHeadings",
                column: "FeeHeadingGroups_FeeHeadingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GuardianDetails_Student_StudentId",
                table: "GuardianDetails",
                column: "Student_StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PastSchoolingReports_Student_StudentId",
                table: "PastSchoolingReports",
                column: "Student_StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRemoteAccesses_StudentRefId",
                table: "StudentRemoteAccesses",
                column: "StudentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTcDetails_StudentId",
                table: "StudentTcDetails",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Declaration_ScholarNumber",
                table: "tbl_Declaration",
                column: "ScholarNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SummerInternship_ScholarNumber",
                table: "tbl_SummerInternship",
                column: "ScholarNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TcFeeDetails_StudentId",
                table: "TcFeeDetails",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TcFeeDetails_StudentTcDetailsId",
                table: "TcFeeDetails",
                column: "StudentTcDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__MigrationHistory");

            migrationBuilder.DropTable(
                name: "AdditionalInformations");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "ClassAndSections");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "ExamTypes");

            migrationBuilder.DropTable(
                name: "FamilyDetails");

            migrationBuilder.DropTable(
                name: "FeeHeadings");

            migrationBuilder.DropTable(
                name: "FeePlans");

            migrationBuilder.DropTable(
                name: "GradingCriterias");

            migrationBuilder.DropTable(
                name: "GuardianDetails");

            migrationBuilder.DropTable(
                name: "LabelControls");

            migrationBuilder.DropTable(
                name: "MasterLabels");

            migrationBuilder.DropTable(
                name: "MasterReports");

            migrationBuilder.DropTable(
                name: "MigrationHistory_10032021");

            migrationBuilder.DropTable(
                name: "MigrationHistory_22032021");

            migrationBuilder.DropTable(
                name: "PastSchoolingReports");

            migrationBuilder.DropTable(
                name: "PeriodSchedules");

            migrationBuilder.DropTable(
                name: "ReportHeadings");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "RolePagePermissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SchoolBoards");

            migrationBuilder.DropTable(
                name: "SMSEMAILSCHEDULEs");

            migrationBuilder.DropTable(
                name: "SMSEMAILSENDHISTORies");

            migrationBuilder.DropTable(
                name: "SMSEMAILTEMPLETES");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "StafsDetails");

            migrationBuilder.DropTable(
                name: "StudentCategorys");

            migrationBuilder.DropTable(
                name: "StudentLoginDetails");

            migrationBuilder.DropTable(
                name: "StudentLoginHistories");

            migrationBuilder.DropTable(
                name: "StudentRegistrationHistories");

            migrationBuilder.DropTable(
                name: "StudentRegNumberMasters");

            migrationBuilder.DropTable(
                name: "StudentRemoteAccesses");

            migrationBuilder.DropTable(
                name: "StudentResetPasswords");

            migrationBuilder.DropTable(
                name: "StudentsRegistrations");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "tbl_AcademicDetail");

            migrationBuilder.DropTable(
                name: "Tbl_AccountSummary");

            migrationBuilder.DropTable(
                name: "Tbl_AccountType");

            migrationBuilder.DropTable(
                name: "Tbl_ArchieveChangeStaffAccounttype");

            migrationBuilder.DropTable(
                name: "Tbl_ArchieveStaffSalary");

            migrationBuilder.DropTable(
                name: "Tbl_Arrear");

            migrationBuilder.DropTable(
                name: "Tbl_Assignment");

            migrationBuilder.DropTable(
                name: "Tbl_BasicPayDetails");

            migrationBuilder.DropTable(
                name: "Tbl_BasicpayMaster");

            migrationBuilder.DropTable(
                name: "Tbl_Batches");

            migrationBuilder.DropTable(
                name: "Tbl_BloodGroup");

            migrationBuilder.DropTable(
                name: "Tbl_Caste");

            migrationBuilder.DropTable(
                name: "Tbl_Category");

            migrationBuilder.DropTable(
                name: "Tbl_Class");

            migrationBuilder.DropTable(
                name: "Tbl_Classsetup");

            migrationBuilder.DropTable(
                name: "Tbl_ClassSubject");

            migrationBuilder.DropTable(
                name: "tbl_CommonDataListItem");

            migrationBuilder.DropTable(
                name: "Tbl_CoScholastic");

            migrationBuilder.DropTable(
                name: "Tbl_CoScholastic_Result");

            migrationBuilder.DropTable(
                name: "Tbl_CoScholasticClass");

            migrationBuilder.DropTable(
                name: "tbl_CoScholasticObtainedGrade");

            migrationBuilder.DropTable(
                name: "Tbl_CreateBank");

            migrationBuilder.DropTable(
                name: "Tbl_CreateBranch");

            migrationBuilder.DropTable(
                name: "Tbl_CreateMerchantId");

            migrationBuilder.DropTable(
                name: "Tbl_DataList");

            migrationBuilder.DropTable(
                name: "Tbl_DataListItem");

            migrationBuilder.DropTable(
                name: "tbl_Declaration");

            migrationBuilder.DropTable(
                name: "Tbl_Deductions");

            migrationBuilder.DropTable(
                name: "tbl_Department");

            migrationBuilder.DropTable(
                name: "Tbl_EPFStatement");

            migrationBuilder.DropTable(
                name: "Tbl_ExamTypes");

            migrationBuilder.DropTable(
                name: "Tbl_MenuName");

            migrationBuilder.DropTable(
                name: "Tbl_MerchantName");

            migrationBuilder.DropTable(
                name: "tbl_PaymentTransactionDetails");

            migrationBuilder.DropTable(
                name: "tbl_PaymentTransactionFeeDetails");

            migrationBuilder.DropTable(
                name: "Tbl_Portions");

            migrationBuilder.DropTable(
                name: "Tbl_Religion");

            migrationBuilder.DropTable(
                name: "Tbl_Remark");

            migrationBuilder.DropTable(
                name: "Tbl_Revision");

            migrationBuilder.DropTable(
                name: "Tbl_RolePermissionNew");

            migrationBuilder.DropTable(
                name: "Tbl_Room");

            migrationBuilder.DropTable(
                name: "Tbl_RoomType");

            migrationBuilder.DropTable(
                name: "Tbl_SalaryStatement");

            migrationBuilder.DropTable(
                name: "Tbl_SchoolSetup");

            migrationBuilder.DropTable(
                name: "Tbl_SectionSetup");

            migrationBuilder.DropTable(
                name: "tbl_Semester");

            migrationBuilder.DropTable(
                name: "Tbl_SetTime");

            migrationBuilder.DropTable(
                name: "Tbl_Siblings");

            migrationBuilder.DropTable(
                name: "tbl_skillset");

            migrationBuilder.DropTable(
                name: "Tbl_StaffAttendance");

            migrationBuilder.DropTable(
                name: "Tbl_StaffCategory");

            migrationBuilder.DropTable(
                name: "Tbl_StaffSalary");

            migrationBuilder.DropTable(
                name: "Tbl_Student_ElectiveRecord");

            migrationBuilder.DropTable(
                name: "Tbl_StudentAttendance");

            migrationBuilder.DropTable(
                name: "Tbl_StudentPromote");

            migrationBuilder.DropTable(
                name: "Tbl_SubjectsSetup");

            migrationBuilder.DropTable(
                name: "Tbl_SubmenuName");

            migrationBuilder.DropTable(
                name: "tbl_SummerInternship");

            migrationBuilder.DropTable(
                name: "Tbl_TcAmount");

            migrationBuilder.DropTable(
                name: "Tbl_TeacherAllocation");

            migrationBuilder.DropTable(
                name: "Tbl_Term");

            migrationBuilder.DropTable(
                name: "Tbl_TestObtainedMark");

            migrationBuilder.DropTable(
                name: "Tbl_TestRecords");

            migrationBuilder.DropTable(
                name: "Tbl_Tests");

            migrationBuilder.DropTable(
                name: "Tbl_TimeTable");

            migrationBuilder.DropTable(
                name: "Tbl_TransportKm");

            migrationBuilder.DropTable(
                name: "Tbl_UserManagement");

            migrationBuilder.DropTable(
                name: "Tbl_WeekDays");

            migrationBuilder.DropTable(
                name: "tbl_WorkExperience");

            migrationBuilder.DropTable(
                name: "TblCreateSchools");

            migrationBuilder.DropTable(
                name: "TblDueFees");

            migrationBuilder.DropTable(
                name: "TblEmailArchieves");

            migrationBuilder.DropTable(
                name: "TblFeeReceipts");

            migrationBuilder.DropTable(
                name: "TblLateFees");

            migrationBuilder.DropTable(
                name: "TblStudentFeeSaveds");

            migrationBuilder.DropTable(
                name: "TblTestAssignDate");

            migrationBuilder.DropTable(
                name: "TblTransportFeeReceipts");

            migrationBuilder.DropTable(
                name: "TblTransportReducedAmounts");

            migrationBuilder.DropTable(
                name: "TblUserDynamicConfigurations");

            migrationBuilder.DropTable(
                name: "TcFeeDetails");

            migrationBuilder.DropTable(
                name: "TermClassMapping");

            migrationBuilder.DropTable(
                name: "TimeSettings");

            migrationBuilder.DropTable(
                name: "TransportFeeConfigurations");

            migrationBuilder.DropTable(
                name: "TransportFeeHeadings");

            migrationBuilder.DropTable(
                name: "TransportFeePlans");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "FeeHeadingGroups");

            migrationBuilder.DropTable(
                name: "Frequencys");

            migrationBuilder.DropTable(
                name: "tbl_StudentDetail");

            migrationBuilder.DropTable(
                name: "StudentTcDetails");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
