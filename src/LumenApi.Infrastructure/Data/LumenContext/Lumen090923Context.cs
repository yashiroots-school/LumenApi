using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LumenApi.Infrastructure.Data.LumenContext;
using LumenApi.Web.Models.Params;

namespace LumenApi.Web;

//public class ApplicationUser : IdentityUser
//{
//  public int DepartmentId { get; set; }
//  public int UserId { get; set; }
//  public bool IsEnable { get; set; }
//  public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
//  {
//    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//    // Add custom user claims here
//    return userIdentity;
//  }
//}
public partial class Lumen090923Context : DbContext///IdentityDbContext<ApplicationUser>
{
  private IConfiguration _configuration;
 

  public Lumen090923Context(DbContextOptions<Lumen090923Context> options, IConfiguration configuration)
      : base(options)
  {
    _configuration = configuration;
  }

  public virtual DbSet<Account> Accounts { get; set; }

  public virtual DbSet<AdditionalInformation> AdditionalInformations { get; set; }

  public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

  public virtual DbSet<AspNetUser> AspNetUsers { get; set; }


  public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

  public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

  public virtual DbSet<Class> Classes { get; set; }

  public virtual DbSet<ClassAndSection> ClassAndSections { get; set; }

  public virtual DbSet<Classroom> Classrooms { get; set; }

  public virtual DbSet<Department> Departments { get; set; }

  public virtual DbSet<Department1> Departments1 { get; set; }

  public virtual DbSet<ExamType> ExamTypes { get; set; }

  public virtual DbSet<FamilyDetail> FamilyDetails { get; set; }

  public virtual DbSet<FeeHeading> FeeHeadings { get; set; }

  public virtual DbSet<FeeHeadingGroup> FeeHeadingGroups { get; set; }

  public virtual DbSet<FeePlan> FeePlans { get; set; }

  public virtual DbSet<Frequency> Frequencys { get; set; }

  public virtual DbSet<GradingCriteria> GradingCriterias { get; set; }

  public virtual DbSet<GuardianDetail> GuardianDetails { get; set; }

  public virtual DbSet<LabelControl> LabelControls { get; set; }

  public virtual DbSet<MasterLabel> MasterLabels { get; set; }

  public virtual DbSet<MasterReport> MasterReports { get; set; }

  public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

  public virtual DbSet<MigrationHistory10032021> MigrationHistory10032021s { get; set; }

  public virtual DbSet<MigrationHistory22032021> MigrationHistory22032021s { get; set; }

  public virtual DbSet<PastSchoolingReport> PastSchoolingReports { get; set; }

  public virtual DbSet<PeriodSchedule> PeriodSchedules { get; set; }

  public virtual DbSet<ReportHeading> ReportHeadings { get; set; }

  public virtual DbSet<RolePagePermission> RolePagePermissions { get; set; }

  public virtual DbSet<SchoolBoard> SchoolBoards { get; set; }

  public virtual DbSet<Smsemailschedule> Smsemailschedules { get; set; }

  public virtual DbSet<Smsemailsendhistory> Smsemailsendhistories { get; set; }

  public virtual DbSet<Smsemailtemplete> Smsemailtempletes { get; set; }

  public virtual DbSet<Staff> Staff { get; set; }

  public virtual DbSet<StafsDetail> StafsDetails { get; set; }

  public virtual DbSet<Student> Students { get; set; }

  public virtual DbSet<StudentCategory> StudentCategorys { get; set; }

  public virtual DbSet<StudentLoginDetail> StudentLoginDetails { get; set; }

  public virtual DbSet<StudentLoginHistory> StudentLoginHistories { get; set; }

  public virtual DbSet<StudentRegNumberMaster> StudentRegNumberMasters { get; set; }

  public virtual DbSet<StudentRegistrationHistory> StudentRegistrationHistories { get; set; }

  public virtual DbSet<StudentRemoteAccess> StudentRemoteAccesses { get; set; }

  public virtual DbSet<StudentResetPassword> StudentResetPasswords { get; set; }

  public virtual DbSet<StudentTcDetail> StudentTcDetails { get; set; }

  public virtual DbSet<StudentsRegistration> StudentsRegistrations { get; set; }

  public virtual DbSet<Subject> Subjects { get; set; }

  public virtual DbSet<TblAcademicDetail> TblAcademicDetails { get; set; }

  public virtual DbSet<TblAccountSummary> TblAccountSummaries { get; set; }

  public virtual DbSet<TblAccountType> TblAccountTypes { get; set; }

  public virtual DbSet<TblArchieveChangeStaffAccounttype> TblArchieveChangeStaffAccounttypes { get; set; }

  public virtual DbSet<TblArchieveStaffSalary> TblArchieveStaffSalaries { get; set; }

  public virtual DbSet<TblArrear> TblArrears { get; set; }

  public virtual DbSet<TblAssignment> TblAssignments { get; set; }

  public virtual DbSet<TblBasicPayDetail> TblBasicPayDetails { get; set; }

  public virtual DbSet<TblBasicpayMaster> TblBasicpayMasters { get; set; }

  public virtual DbSet<TblBatch> TblBatches { get; set; }

  public virtual DbSet<TblBloodGroup> TblBloodGroups { get; set; }

  public virtual DbSet<TblCaste> TblCastes { get; set; }

  public virtual DbSet<TblCategory> TblCategories { get; set; }

  public virtual DbSet<TblClass> TblClasses { get; set; }

  public  DbSet<TblClassSubjects> TblClassSubjects { get; set; }

  public virtual DbSet<TblClasssetup> TblClasssetups { get; set; }

  public  DbSet<TblCoScholastic> TblCoScholastics { get; set; }

  public  DbSet<TblCoScholasticClass> TblCoScholasticClasses { get; set; }

  public virtual DbSet<TblCoScholasticObtainedGrade> TblCoScholasticObtainedGrades { get; set; }

  public virtual DbSet<TblCoScholasticResult> TblCoScholasticResults { get; set; }

  public virtual DbSet<TblCommonDataListItem> TblCommonDataListItems { get; set; }

  public virtual DbSet<Tbl_CreateBank> TblCreateBanks { get; set; }

  public virtual DbSet<Tbl_CreateBranch> TblCreateBranches { get; set; }

  public virtual DbSet<Tbl_CreateMerchantId> TblCreateMerchantIds { get; set; }

  public virtual DbSet<TblCreateSchool> TblCreateSchools { get; set; }

  public virtual DbSet<TblDataList> TblDataLists { get; set; }

  public virtual DbSet<TblDataListItem> TblDataListItems { get; set; }

  public virtual DbSet<TblDeclaration> TblDeclarations { get; set; }

  public virtual DbSet<TblDeduction> TblDeductions { get; set; }

  public virtual DbSet<TblDepartment> TblDepartments { get; set; }

  public virtual DbSet<TblDueFee> TblDueFees { get; set; }

  public virtual DbSet<TblEmailArchiefe> TblEmailArchieves { get; set; }

  public virtual DbSet<TblEpfstatement> TblEpfstatements { get; set; }

  public virtual DbSet<TblExamType> TblExamTypes { get; set; }

  public virtual DbSet<TblFeeReceipt> TblFeeReceipts { get; set; }

  public virtual DbSet<TblLateFee> TblLateFees { get; set; }

  public virtual DbSet<TblMenuName> TblMenuNames { get; set; }

  public virtual DbSet<Tbl_MerchantName> TblMerchantNames { get; set; }

  public virtual DbSet<tbl_PaymentTransactionDetails> TblPaymentTransactionDetails { get; set; }

  public virtual DbSet<TblPaymentTransactionFeeDetail> TblPaymentTransactionFeeDetails { get; set; }

  public virtual DbSet<TblPortion> TblPortions { get; set; }

  public virtual DbSet<TblReligion> TblReligions { get; set; }

  public virtual DbSet<TblRemark> TblRemarks { get; set; }

  public virtual DbSet<TblRevision> TblRevisions { get; set; }

  public virtual DbSet<TblRolePermissionNew> TblRolePermissionNews { get; set; }

  public virtual DbSet<TblRoom> TblRooms { get; set; }

  public virtual DbSet<TblRoomType> TblRoomTypes { get; set; }

  public virtual DbSet<TblSalaryStatement> TblSalaryStatements { get; set; }

  public virtual DbSet<Tbl_SchoolSetup> TblSchoolSetups { get; set; }

  public virtual DbSet<TblSectionSetup> TblSectionSetups { get; set; }

  public virtual DbSet<TblSemester> TblSemesters { get; set; }

  public virtual DbSet<TblSetTime> TblSetTimes { get; set; }

  public virtual DbSet<TblSibling> TblSiblings { get; set; }

  public virtual DbSet<TblSkillset> TblSkillsets { get; set; }

  public virtual DbSet<TblStaffAttendance> TblStaffAttendances { get; set; }

  public virtual DbSet<TblStaffCategory> TblStaffCategories { get; set; }

  public virtual DbSet<TblStaffSalary> TblStaffSalaries { get; set; }

  public virtual DbSet<TblStudentAttendance> TblStudentAttendances { get; set; }
  public virtual DbSet<MobileAppVersions> MobileAppVersion { get; set; }

  public virtual DbSet<TblStudentDetail> TblStudentDetails { get; set; }

  public virtual DbSet<TblStudentElectiveRecord> TblStudentElectiveRecord { get; set; }

  public virtual DbSet<TblStudentFeeSaved> TblStudentFeeSaveds { get; set; }

  public virtual DbSet<TblStudentPromote> TblStudentPromotes { get; set; }

  public virtual DbSet<TblSubjectsSetup> TblSubjectsSetups { get; set; }

  public virtual DbSet<TblSubmenuName> TblSubmenuNames { get; set; }

  public virtual DbSet<TblSummerInternship> TblSummerInternships { get; set; }

  public virtual DbSet<TblTcAmount> TblTcAmounts { get; set; }

  public virtual DbSet<TblTeacherAllocation> TblTeacherAllocations { get; set; }

  public virtual DbSet<TblTerm> TblTerms { get; set; }

  public virtual DbSet<TblTests> TblTests { get; set; }

  public virtual DbSet<TblTestAssignDates> TblTestAssignDates { get; set; }

  public  DbSet<TblTestObtainedMark> TblTestObtainedMarks { get; set; }

  public virtual DbSet<TblTestRecord> TblTestRecords { get; set; }

  public virtual DbSet<TblTimeTable> TblTimeTables { get; set; }

  public virtual DbSet<TblTransportFeeReceipt> TblTransportFeeReceipts { get; set; }

  public virtual DbSet<TblTransportKm> TblTransportKms { get; set; }

  public virtual DbSet<TblTransportReducedAmount> TblTransportReducedAmounts { get; set; }

  public virtual DbSet<TblUserDynamicConfiguration> TblUserDynamicConfigurations { get; set; }

  public virtual DbSet<TblUserManagement> TblUserManagements { get; set; }

  public virtual DbSet<TblWeekDay> TblWeekDays { get; set; }

  public virtual DbSet<TblWorkExperience> TblWorkExperiences { get; set; }

  public virtual DbSet<TcFeeDetail> TcFeeDetails { get; set; }

  public virtual DbSet<TermClassMapping> TermClassMappings { get; set; }

  public virtual DbSet<TimeSetting> TimeSettings { get; set; }

  public virtual DbSet<TransportFeeConfiguration> TransportFeeConfigurations { get; set; }

  public virtual DbSet<TransportFeeHeading> TransportFeeHeadings { get; set; }

  public virtual DbSet<TransportFeePlan> TransportFeePlans { get; set; }


  public virtual DbSet<Tbl_PublishDetail> Tbl_PublishDetail { get; set; }
  //public virtual DbSet<Tbl_Assignment> TblAssignments { get; set; }
  public virtual DbSet<Tbl_FreezeData> Tbl_FreezeData { get; set; }
  public virtual DbSet<Tbl_CreateMerchantId> Tbl_CreateMerchantId { get; set; }
  public virtual DbSet<tbl_PaymentTransactionDetails> tbl_PaymentTransactionDetails { get; set; }
  public DbSet<Tbl_MerchantName> Tbl_MerchantName { get; set; }
  
  public DbSet<Tbl_SchoolSetup> Tbl_SchoolSetup { get; set; }

  public DbSet<Tbl_CreateBank> Tbl_CreateBank { get; set; }
  public DbSet<Tbl_CreateBranch> Tbl_CreateBranch { get; set; }
  public DbSet<Tbl_HoldDetail> Tbl_HoldDetail { get; set; }
  public DbSet<Tbl_FireBaseToken> Tbl_FireBaseToken { get; set; }
  public DbSet<Tbl_NotificationLog> Tbl_NotificationLog { get; set; }
  //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
  //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //    => optionsBuilder.UseSqlServer("Server=PRG1131\\SQLEXPRESS;Database=lumen090923;Trusted_Connection=True;TrustServerCertificate=true;integrated security=true;Command Timeout=3600;");

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    try
    {
      //"Server=PRG1131\\SQLEXPRESS;Database=lumen090923;Trusted_Connection=True;TrustServerCertificate=true;integrated security=true;"
      //string connctionStr = "Data source=198.12.225.42;Initial Catalog=lumenDemo;Integrated Security=true;TrustServerCertificate=True";
      string connctionStr = _configuration.GetConnectionString("DefaultConnection")!;//"Server=PRG1131\\SQLEXPRESS;Database=lumen090923;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=true;";
      optionsBuilder.UseSqlServer(connctionStr);
      optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
      optionsBuilder.UseSqlServer(connctionStr, builder =>
      {
        //builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

      });
      base.OnConfiguring(optionsBuilder);
    }
    catch (Exception )
    {

      throw;
    }
   
  }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Account>(entity =>
    {
      entity.HasKey(e => e.AccountId).HasName("PK_dbo.Accounts");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AdditionalInformation>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.AdditionalInformations");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
      entity.Property(e => e.Grade).HasColumnName("grade");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.SectionName).HasColumnName("Section_Name");
      entity.Property(e => e.StudentStudentId).HasColumnName("Student_StudentId");

      entity.HasOne(d => d.StudentStudent).WithMany(p => p.AdditionalInformations)
              .HasForeignKey(d => d.StudentStudentId)
              .HasConstraintName("FK_dbo.AdditionalInformations_dbo.Students_Student_StudentId");
    });

    modelBuilder.Entity<AspNetRole>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetRoles");

      entity.Property(e => e.Id).HasMaxLength(128);
      entity.Property(e => e.Name).HasMaxLength(256);
    });

    modelBuilder.Entity<AspNetUser>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUsers");

      entity.Property(e => e.Id).HasMaxLength(128);
      entity.Property(e => e.Email).HasMaxLength(256);
      entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");
      entity.Property(e => e.UserName).HasMaxLength(256);

      entity.HasMany(d => d.Roles).WithMany(p => p.Users)
              .UsingEntity<Dictionary<string, object>>(
                  "AspNetUserRole",
                  r => r.HasOne<AspNetRole>().WithMany()
                      .HasForeignKey("RoleId")
                      .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId"),
                  l => l.HasOne<AspNetUser>().WithMany()
                      .HasForeignKey("UserId")
                      .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId"),
                  j =>
                  {
                    j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");
                    j.ToTable("AspNetUserRoles");
                    j.IndexerProperty<string>("UserId").HasMaxLength(128);
                    j.IndexerProperty<string>("RoleId").HasMaxLength(128);
                  });
    });

    modelBuilder.Entity<AspNetUserClaim>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUserClaims");

      entity.Property(e => e.UserId).HasMaxLength(128);

      entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims)
              .HasForeignKey(d => d.UserId)
              .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
    });

    modelBuilder.Entity<AspNetUserLogin>(entity =>
    {
      entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId }).HasName("PK_dbo.AspNetUserLogins");

      entity.Property(e => e.LoginProvider).HasMaxLength(128);
      entity.Property(e => e.ProviderKey).HasMaxLength(128);
      entity.Property(e => e.UserId).HasMaxLength(128);

      entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins)
              .HasForeignKey(d => d.UserId)
              .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
    });


    modelBuilder.Entity<Class>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.Classes");
    });

    modelBuilder.Entity<ClassAndSection>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.ClassAndSections");
    });

    modelBuilder.Entity<Classroom>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.Classrooms");

      entity.Property(e => e.ClassName).HasColumnName("className");
    });

    modelBuilder.Entity<Department>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC071BFD2C07");

      entity.ToTable("Department");

      entity.Property(e => e.Name).HasMaxLength(50);
    });

    modelBuilder.Entity<Department1>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.Departments");

      entity.ToTable("Departments");

      entity.Property(e => e.Id).ValueGeneratedNever();
    });

    modelBuilder.Entity<ExamType>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.ExamTypes");

      entity.Property(e => e.ExamType1).HasColumnName("ExamType");
    });

    modelBuilder.Entity<FamilyDetail>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.FamilyDetails");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ApplicationNumber).HasMaxLength(100);
      entity.Property(e => e.FannualIncome).HasColumnName("FAnnualIncome");
      entity.Property(e => e.Femail).HasColumnName("FEMail");
      entity.Property(e => e.Fmobile).HasColumnName("FMobile");
      entity.Property(e => e.Foccupation).HasColumnName("FOccupation");
      entity.Property(e => e.Forganization).HasColumnName("FOrganization");
      entity.Property(e => e.Fphone).HasColumnName("FPhone");
      entity.Property(e => e.Fqualifications).HasColumnName("FQualifications");
      entity.Property(e => e.FresidentialAddress).HasColumnName("FResidentialAddress");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.MannualIncome).HasColumnName("MAnnualIncome");
      entity.Property(e => e.Memail).HasColumnName("MEMail");
      entity.Property(e => e.Mmobile).HasColumnName("MMobile");
      entity.Property(e => e.Moccupation).HasColumnName("MOccupation");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.Morganization).HasColumnName("MOrganization");
      entity.Property(e => e.MpermanentAddress).HasColumnName("MPermanentAddress");
      entity.Property(e => e.Mphone).HasColumnName("MPhone");
      entity.Property(e => e.Mqualifications).HasColumnName("MQualifications");
      entity.Property(e => e.StudentStudentId)
              .HasMaxLength(100)
              .HasColumnName("Student_StudentId");
    });

    modelBuilder.Entity<FeeHeading>(entity =>
    {
      entity.HasKey(e => e.FeeId).HasName("PK_dbo.FeeHeadings");

      entity.Property(e => e.AccountsAccountId).HasColumnName("Accounts_AccountId");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.FeeHeadingGroupsFeeHeadingGroupId).HasColumnName("FeeHeadingGroups_FeeHeadingGroupId");
      entity.Property(e => e.FeeTypeId).HasColumnName("FeeType_Id");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

      entity.HasOne(d => d.AccountsAccount).WithMany(p => p.FeeHeadings)
              .HasForeignKey(d => d.AccountsAccountId)
              .HasConstraintName("FK_dbo.FeeHeadings_dbo.Accounts_Accounts_AccountId");

      entity.HasOne(d => d.FeeFrequency).WithMany(p => p.FeeHeadings)
              .HasForeignKey(d => d.FeeFrequencyId)
              .HasConstraintName("FK_dbo.FeeHeadings_dbo.Frequencys_FeeFrequencyId");

      entity.HasOne(d => d.FeeHeadingGroupsFeeHeadingGroup).WithMany(p => p.FeeHeadings)
              .HasForeignKey(d => d.FeeHeadingGroupsFeeHeadingGroupId)
              .HasConstraintName("FK_dbo.FeeHeadings_dbo.FeeHeadingGroups_FeeHeadingGroups_FeeHeadingGroupId");
    });

    modelBuilder.Entity<FeeHeadingGroup>(entity =>
    {
      entity.HasKey(e => e.FeeHeadingGroupId).HasName("PK_dbo.FeeHeadingGroups");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FeePlan>(entity =>
    {
      entity.HasKey(e => e.FeePlanId).HasName("PK_dbo.FeePlans");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.BatchName1).HasColumnName("Batch_Name");
      entity.Property(e => e.FeeConfigurationid).HasColumnName("Fee_configurationid");
      entity.Property(e => e.FeeTypeId).HasColumnName("FeeType_Id");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.KmDistanceId).HasColumnName("KmDistance_Id");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.TransportOptId).HasColumnName("TransportOpt_Id");
    });

    modelBuilder.Entity<Frequency>(entity =>
    {
      entity.HasKey(e => e.FeeFrequencyId).HasName("PK_dbo.Frequencys");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<GradingCriteria>(entity =>
    {
      entity.HasKey(e => e.CriteriaId).HasName("PK_GradingCriteria");

      entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");
      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.Grade)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.GradeDescription)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.MaximumPercentage).HasColumnType("decimal(18, 1)");
      entity.Property(e => e.MinimumPercentage).HasColumnType("decimal(18, 1)");
    });

    modelBuilder.Entity<GuardianDetail>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.GuardianDetails");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Email).HasColumnName("EMail");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.StudentStudentId).HasColumnName("Student_StudentId");

      entity.HasOne(d => d.StudentStudent).WithMany(p => p.GuardianDetails)
              .HasForeignKey(d => d.StudentStudentId)
              .HasConstraintName("FK_dbo.GuardianDetails_dbo.Students_Student_StudentId");
    });

    modelBuilder.Entity<LabelControl>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.LabelControls");

      entity.Property(e => e.SchoolId).HasColumnName("School_Id");
    });

    modelBuilder.Entity<MasterLabel>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.MasterLabels");

      entity.Property(e => e.SubMenuId).HasColumnName("SubMenu_Id");
    });

    modelBuilder.Entity<MasterReport>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.MasterReports");

      entity.Property(e => e.CreatedAt).HasColumnType("datetime");
      entity.Property(e => e.UpdateAt).HasColumnType("datetime");
    });

    modelBuilder.Entity<MigrationHistory>(entity =>
    {
      entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

      entity.ToTable("__MigrationHistory");

      entity.Property(e => e.MigrationId).HasMaxLength(150);
      entity.Property(e => e.ContextKey).HasMaxLength(300);
      entity.Property(e => e.ProductVersion).HasMaxLength(32);
    });

    modelBuilder.Entity<MigrationHistory10032021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MigrationHistory_10032021");

      entity.Property(e => e.ContextKey).HasMaxLength(300);
      entity.Property(e => e.MigrationId).HasMaxLength(150);
      entity.Property(e => e.ProductVersion).HasMaxLength(32);
    });

    modelBuilder.Entity<MigrationHistory22032021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MigrationHistory_22032021");

      entity.Property(e => e.ContextKey).HasMaxLength(300);
      entity.Property(e => e.MigrationId).HasMaxLength(150);
      entity.Property(e => e.ProductVersion).HasMaxLength(32);
    });

    modelBuilder.Entity<PastSchoolingReport>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.PastSchoolingReports");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.StudentStudentId).HasColumnName("Student_StudentId");
      entity.Property(e => e.Tcavatar).HasColumnName("TCAvatar");

      entity.HasOne(d => d.StudentStudent).WithMany(p => p.PastSchoolingReports)
              .HasForeignKey(d => d.StudentStudentId)
              .HasConstraintName("FK_dbo.PastSchoolingReports_dbo.Students_Student_StudentId");
    });

    modelBuilder.Entity<PeriodSchedule>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.PeriodSchedules");
    });

    modelBuilder.Entity<ReportHeading>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.ReportHeadings");
    });

    modelBuilder.Entity<RolePagePermission>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.RolePagePermissions");
    });

    modelBuilder.Entity<SchoolBoard>(entity =>
    {
      entity.HasKey(e => e.BoardId);

      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.BoardName)
              .HasMaxLength(200)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Smsemailschedule>(entity =>
    {
      entity.HasKey(e => e.Smsemailscheduleid).HasName("PK_dbo.SMSEMAILSCHEDULEs");

      entity.ToTable("SMSEMAILSCHEDULEs");

      entity.Property(e => e.Smsemailscheduleid).HasColumnName("SMSEMAILSCHEDULEID");
      entity.Property(e => e.Createddate)
              .HasColumnType("datetime")
              .HasColumnName("CREATEDDATE");
      entity.Property(e => e.Scheduletype).HasColumnName("SCHEDULETYPE");
    });

    modelBuilder.Entity<Smsemailsendhistory>(entity =>
    {
      entity.HasKey(e => e.Historyid).HasName("PK_dbo.SMSEMAILSENDHISTORies");

      entity.ToTable("SMSEMAILSENDHISTORies");

      entity.Property(e => e.Historyid).HasColumnName("HISTORYID");
      entity.Property(e => e.Attachedfile).HasColumnName("ATTACHEDFILE");
      entity.Property(e => e.Attachedfilename).HasColumnName("ATTACHEDFILENAME");
      entity.Property(e => e.Attachedfiletype).HasColumnName("ATTACHEDFILETYPE");
      entity.Property(e => e.Createddate)
              .HasColumnType("datetime")
              .HasColumnName("CREATEDDATE");
      entity.Property(e => e.Email).HasColumnName("EMAIL");
      entity.Property(e => e.Senderid).HasColumnName("SENDERID");
      entity.Property(e => e.Sendertype).HasColumnName("SENDERTYPE");
      entity.Property(e => e.Sms).HasColumnName("SMS");
      entity.Property(e => e.Subject).HasColumnName("SUBJECT");
    });

    modelBuilder.Entity<Smsemailtemplete>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("SMSEMAILTEMPLETES");

      entity.Property(e => e.Attachedfile)
              .HasMaxLength(800)
              .HasColumnName("ATTACHEDFILE");
      entity.Property(e => e.Attachedfilename)
              .HasMaxLength(100)
              .HasColumnName("ATTACHEDFILENAME");
      entity.Property(e => e.Attachedfiletype)
              .HasMaxLength(50)
              .HasColumnName("ATTACHEDFILETYPE");
      entity.Property(e => e.Createddate).HasColumnName("CREATEDDATE");
      entity.Property(e => e.Email).HasColumnName("EMAIL");
      entity.Property(e => e.Notificationtype)
              .HasMaxLength(10)
              .HasColumnName("NOTIFICATIONTYPE");
      entity.Property(e => e.Sms).HasColumnName("SMS");
      entity.Property(e => e.Smsemailid)
              .ValueGeneratedOnAdd()
              .HasColumnName("SMSEMAILID");
      entity.Property(e => e.Smssubject).HasColumnName("SMSSubject");
      entity.Property(e => e.Subject)
              .HasMaxLength(500)
              .HasColumnName("SUBJECT");
    });

    modelBuilder.Entity<Staff>(entity =>
    {
      entity.HasKey(e => e.Staffid).HasName("PK__staff__645AE4A6656C112C");

      entity.ToTable("staff");

      entity.Property(e => e.Staffid)
              .ValueGeneratedNever()
              .HasColumnName("staffid");
      entity.Property(e => e.Staffaddress)
              .HasMaxLength(200)
              .HasColumnName("staffaddress");
      entity.Property(e => e.Staffname)
              .HasMaxLength(100)
              .HasColumnName("staffname");
    });

    modelBuilder.Entity<StafsDetail>(entity =>
    {
      entity.HasKey(e => e.StafId).HasName("PK_dbo.StafsDetails");

      entity.Property(e => e.AccountNo).HasColumnName("Account_No");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.BankAcno).HasColumnName("BankACNo");
      entity.Property(e => e.BankName).HasColumnName("Bank_Name");
      entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
      entity.Property(e => e.Dob).HasColumnName("DOB");
      entity.Property(e => e.EmployeeAccountId).HasColumnName("Employee_AccountId");
      entity.Property(e => e.EmployeeAccountName).HasColumnName("Employee_AccountName");
      entity.Property(e => e.EmployeeDesignation).HasColumnName("Employee_Designation");
      entity.Property(e => e.IfscCode).HasColumnName("IFSC_Code");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.Pob).HasColumnName("POB");
      entity.Property(e => e.StaffCategoryName).HasColumnName("Staff_CategoryName");
      entity.Property(e => e.Uan).HasColumnName("UAN");
      entity.Property(e => e.Uin).HasColumnName("UIN");
    });

    modelBuilder.Entity<Student>(entity =>
    {
      entity.HasKey(e => e.StudentId).HasName("PK_dbo.Students");

      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroup_Id");
      entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.Dob).HasColumnName("DOB");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.IsApplyforTc).HasColumnName("IsApplyforTC");
      entity.Property(e => e.IsPromoted)
              .HasDefaultValue(true)
              .HasColumnName("isPromoted");
      entity.Property(e => e.LastName).HasColumnName("Last_Name");
      entity.Property(e => e.Pob).HasColumnName("POB");
      entity.Property(e => e.RegNumber).HasMaxLength(100);
      entity.Property(e => e.Rte).HasColumnName("RTE");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.TransportOptions).HasColumnName("Transport_Options");
      entity.Property(e => e.Uin).HasColumnName("UIN");
    });

    modelBuilder.Entity<StudentCategory>(entity =>
    {
      entity.HasKey(e => e.CategoryId).HasName("PK_dbo.StudentCategorys");
    });

    modelBuilder.Entity<StudentLoginDetail>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__StudentL__3214EC076EF57B66");

      entity.Property(e => e.CreatedOn).HasColumnType("datetime");
      entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
      entity.Property(e => e.UserName).HasMaxLength(200);
      entity.Property(e => e.UserPassword).HasMaxLength(200);
    });

    modelBuilder.Entity<StudentLoginHistory>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.StudentLoginHistories");

      entity.Property(e => e.CreatedOn).HasColumnType("datetime");
    });

    modelBuilder.Entity<StudentRegNumberMaster>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.BatchName).HasMaxLength(100);
      entity.Property(e => e.Class).HasMaxLength(100);
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.CreatedOn).HasColumnType("datetime");
      entity.Property(e => e.RegPrefix).HasMaxLength(100);
      entity.Property(e => e.RegStatus).HasMaxLength(1);
      entity.Property(e => e.StudnetRegNumberMasterId)
              .ValueGeneratedOnAdd()
              .HasColumnName("StudnetRegNumberMasterID");
    });

    modelBuilder.Entity<StudentRegistrationHistory>(entity =>
    {
      entity.HasKey(e => e.StudentRegisterHistoryId).HasName("PK_dbo.StudentRegistrationHistories");

      entity.Property(e => e.StudentRegisterHistoryId).HasColumnName("StudentRegisterHistoryID");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.Dob).HasColumnName("DOB");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.IsApplyforTc).HasColumnName("IsApplyforTC");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.ParentsEmail).HasColumnName("Parents_Email");
      entity.Property(e => e.Pob).HasColumnName("POB");
      entity.Property(e => e.Rte).HasColumnName("RTE");
      entity.Property(e => e.StudentRegisterId).HasColumnName("StudentRegisterID");
      entity.Property(e => e.Uin).HasColumnName("UIN");
    });

    modelBuilder.Entity<StudentRemoteAccess>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.StudentRemoteAccesses");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

      entity.HasOne(d => d.StudentRef).WithMany(p => p.StudentRemoteAccesses)
              .HasForeignKey(d => d.StudentRefId)
              .HasConstraintName("FK_dbo.StudentRemoteAccesses_dbo.Students_StudentRefId");
    });

    modelBuilder.Entity<StudentResetPassword>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.StudentResetPasswords");

      entity.Property(e => e.CreatedOn).HasColumnType("datetime");
    });

    modelBuilder.Entity<StudentTcDetail>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.StudentTcDetails");

      entity.Property(e => e.CreatedOn).HasColumnType("datetime");
      entity.Property(e => e.ReasonId).HasColumnName("ReasonID");
      entity.Property(e => e.RemarksId).HasColumnName("RemarksID");
      entity.Property(e => e.SchoolLeftDate).HasColumnType("datetime");

      entity.HasOne(d => d.Student).WithMany(p => p.StudentTcDetails)
              .HasForeignKey(d => d.StudentId)
              .HasConstraintName("FK_dbo.StudentTcDetails_dbo.Students_StudentId");
    });

    modelBuilder.Entity<StudentsRegistration>(entity =>
    {
      entity.HasKey(e => e.StudentRegisterId).HasName("PK_dbo.StudentsRegistrations");

      entity.Property(e => e.StudentRegisterId).HasColumnName("StudentRegisterID");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.BankAcholder).HasColumnName("BankACHolder");
      entity.Property(e => e.BankIfsc).HasColumnName("BankIFSC");
      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.BatchName1).HasColumnName("Batch_Name");
      entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroup_Id");
      entity.Property(e => e.CastId).HasColumnName("Cast_Id");
      entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
      entity.Property(e => e.Dob).HasColumnName("DOB");
      entity.Property(e => e.Email).HasMaxLength(50);
      entity.Property(e => e.EmailSend).HasColumnName("Email_send");
      entity.Property(e => e.EmailSendDate).HasColumnName("Email_SendDate");
      entity.Property(e => e.FamilySssmid).HasColumnName("FamilySSSMID");
      entity.Property(e => e.GradeDivision).HasColumnName("Grade_Division");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.IsApplyforTc).HasColumnName("IsApplyforTC");
      entity.Property(e => e.IsRtestudent).HasColumnName("IsRTEStudent");
      entity.Property(e => e.LastName).HasColumnName("Last_Name");
      entity.Property(e => e.LastStudiedSchoolName).HasMaxLength(100);
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.ParentsEmail).HasColumnName("Parents_Email");
      entity.Property(e => e.Pob).HasColumnName("POB");
      entity.Property(e => e.PromotionDate).HasColumnName("Promotion_Date");
      entity.Property(e => e.PromotionYear).HasColumnName("Promotion_Year");
      entity.Property(e => e.RegistrationDate).HasColumnName("Registration_Date");
      entity.Property(e => e.ReligionId).HasColumnName("Religion_Id");
      entity.Property(e => e.Rte).HasColumnName("RTE");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.SectionName).HasColumnName("Section_Name");
      entity.Property(e => e.SssmidNumber).HasColumnName("SSSMIdNumber");
      entity.Property(e => e.TransportOptions).HasColumnName("Transport_Options");
      entity.Property(e => e.Uin).HasColumnName("UIN");
    });

    modelBuilder.Entity<Subject>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.Subjects");

      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassTeacher).HasColumnName("Class_Teacher");
      entity.Property(e => e.Section)
              .HasMaxLength(255)
              .IsUnicode(false);
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.Subject1).HasColumnName("Subject");
      entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");
    });

    modelBuilder.Entity<TblAcademicDetail>(entity =>
    {
      entity.HasKey(e => e.AcademicDetailId).HasName("PK_dbo.tbl_AcademicDetail");

      entity.ToTable("tbl_AcademicDetail");

      entity.Property(e => e.AcademicYear).HasMaxLength(20);
      entity.Property(e => e.Addeby).HasMaxLength(20);
      entity.Property(e => e.Addedon).HasMaxLength(20);
      entity.Property(e => e.Dateon).HasMaxLength(35);
      entity.Property(e => e.Institution).HasMaxLength(1024);
      entity.Property(e => e.Percentage).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Qualification).HasMaxLength(20);
      entity.Property(e => e.ScholarNumber).HasMaxLength(50);
      entity.Property(e => e.Spare1).HasMaxLength(35);
      entity.Property(e => e.Spare2).HasMaxLength(35);
      entity.Property(e => e.Spare3).HasMaxLength(35);
      entity.Property(e => e.Stream).HasMaxLength(256);
      entity.Property(e => e.University).HasMaxLength(1024);
      entity.Property(e => e.Updatedby).HasMaxLength(20);
      entity.Property(e => e.Updatedon).HasMaxLength(20);
    });

    modelBuilder.Entity<TblAccountSummary>(entity =>
    {
      entity.HasKey(e => e.SummaryId).HasName("PK_dbo.Tbl_AccountSummary");

      entity.ToTable("Tbl_AccountSummary");

      entity.Property(e => e.SummaryId).HasColumnName("Summary_Id");
      entity.Property(e => e.AddedDate).HasColumnName("Added_Date");
      entity.Property(e => e.AddedDay).HasColumnName("Added_Day");
      entity.Property(e => e.AddedMonth).HasColumnName("Added_Month");
      entity.Property(e => e.AddedYear).HasColumnName("Added_Year");
      entity.Property(e => e.ArrearAmt).HasColumnName("Arrear_Amt");
      entity.Property(e => e.AttendencePercentage).HasColumnName("Attendence_Percentage");
      entity.Property(e => e.BasicSalary).HasColumnName("Basic_Salary");
      entity.Property(e => e.Cca).HasColumnName("CCA");
      entity.Property(e => e.Da).HasColumnName("DA");
      entity.Property(e => e.DeductionAmt).HasColumnName("Deduction_Amt");
      entity.Property(e => e.EmployeeContribution).HasColumnName("Employee_Contribution");
      entity.Property(e => e.EmployerContribution).HasColumnName("Employer_Contribution");
      entity.Property(e => e.Esi).HasColumnName("ESI");
      entity.Property(e => e.Hra).HasColumnName("HRA");
      entity.Property(e => e.Lop).HasColumnName("LOP");
      entity.Property(e => e.NetPay1).HasColumnName("Net_Pay");
      entity.Property(e => e.OtherAllowance).HasColumnName("OtherALlowance");
      entity.Property(e => e.Pf).HasColumnName("PF");
      entity.Property(e => e.ProfessionalTax).HasColumnName("Professional_Tax");
      entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
      entity.Property(e => e.StaffName).HasColumnName("Staff_Name");
      entity.Property(e => e.TotalSalary).HasColumnName("Total_Salary");
    });

    modelBuilder.Entity<TblAccountType>(entity =>
    {
      entity.HasKey(e => e.AccountTypeId).HasName("PK_dbo.Tbl_AccountType");

      entity.ToTable("Tbl_AccountType");

      entity.Property(e => e.AccountTypeId).HasColumnName("Account_TypeId");
      entity.Property(e => e.AccountTypename).HasColumnName("Account_Typename");
      entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");
    });

    modelBuilder.Entity<TblArchieveChangeStaffAccounttype>(entity =>
    {
      entity.HasKey(e => e.ChangeAccounTypeId).HasName("PK_dbo.Tbl_ArchieveChangeStaffAccounttype");

      entity.ToTable("Tbl_ArchieveChangeStaffAccounttype");

      entity.Property(e => e.ChangeAccounTypeId).HasColumnName("ChangeAccounType_ID");
      entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
      entity.Property(e => e.EmployeeAccountId).HasColumnName("Employee_AccountId");
      entity.Property(e => e.EmployeeAccountName).HasColumnName("Employee_AccountName");
      entity.Property(e => e.EmployeeCode).HasColumnName("Employee_Code");
      entity.Property(e => e.EmployeeDesignation).HasColumnName("Employee_Designation");
      entity.Property(e => e.StafId).HasColumnName("StafID");
      entity.Property(e => e.StafName).HasColumnName("Staf_Name");
      entity.Property(e => e.StaffCategoryName).HasColumnName("Staff_CategoryName");
    });

    modelBuilder.Entity<TblArchieveStaffSalary>(entity =>
    {
      entity.HasKey(e => e.ArchieveId).HasName("PK_dbo.Tbl_ArchieveStaffSalary");

      entity.ToTable("Tbl_ArchieveStaffSalary");

      entity.Property(e => e.ArchieveId).HasColumnName("Archieve_Id");
      entity.Property(e => e.BasicAmount).HasColumnName("Basic_Amount");
      entity.Property(e => e.SalaryAmount).HasColumnName("Salary_Amount");
      entity.Property(e => e.SalaryId).HasColumnName("Salary_Id");
      entity.Property(e => e.StaffId).HasColumnName("Staff_ID");
      entity.Property(e => e.StaffName).HasColumnName("Staff_Name");
    });

    modelBuilder.Entity<TblArrear>(entity =>
    {
      entity.HasKey(e => e.ArrearId).HasName("PK_dbo.Tbl_Arrear");

      entity.ToTable("Tbl_Arrear");

      entity.Property(e => e.ArrearId).HasColumnName("Arrear_Id");
      entity.Property(e => e.AddedDate).HasColumnName("Added_Date");
      entity.Property(e => e.AddedDate1)
              .HasColumnType("datetime")
              .HasColumnName("AddedDate");
      entity.Property(e => e.AddedDay).HasColumnName("Added_Day");
      entity.Property(e => e.AddedMonth).HasColumnName("Added_Month");
      entity.Property(e => e.AddedYear).HasColumnName("Added_Year");
      entity.Property(e => e.ArrearAmt).HasColumnName("Arrear_Amt");
      entity.Property(e => e.DeductionAmt).HasColumnName("Deduction_Amt");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.NetPay).HasColumnName("Net_Pay");
      entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
      entity.Property(e => e.StaffName).HasColumnName("Staff_Name");
    });

    modelBuilder.Entity<TblAssignment>(entity =>
    {
      entity.HasKey(e => e.AssignmentId).HasName("PK_dbo.Tbl_Assignment");

      entity.ToTable("Tbl_Assignment");

      entity.Property(e => e.AssignmentId).HasColumnName("Assignment_Id");
      entity.Property(e => e.AssignmentDate).HasColumnName("Assignment_Date");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
      entity.Property(e => e.NewAssignment).HasColumnName("New_Assignment");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.SectionName).HasColumnName("Section_Name");
      entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");
      entity.Property(e => e.SubjectName).HasColumnName("Subject_Name");
      entity.Property(e => e.SubmittedDate).HasColumnName("Submitted_Date");
    });

    modelBuilder.Entity<TblBasicPayDetail>(entity =>
    {
      entity.HasKey(e => e.BasicAmountId).HasName("PK_dbo.Tbl_BasicPayDetails");

      entity.ToTable("Tbl_BasicPayDetails");

      entity.Property(e => e.BasicAmountId).HasColumnName("BasicAmount_Id");
      entity.Property(e => e.BasicAmount).HasColumnName("Basic_Amount");
      entity.Property(e => e.BasicPayId).HasColumnName("BasicPay_Id");
      entity.Property(e => e.BasicpayName).HasColumnName("Basicpay_Name");
      entity.Property(e => e.CategoryName).HasColumnName("Category_Name");
      entity.Property(e => e.SchoolCategoryId).HasColumnName("SchoolCategory_Id");
    });

    modelBuilder.Entity<TblBasicpayMaster>(entity =>
    {
      entity.HasKey(e => e.BasicPayMasterId).HasName("PK_dbo.Tbl_BasicpayMaster");

      entity.ToTable("Tbl_BasicpayMaster");

      entity.Property(e => e.BasicPayMasterId).HasColumnName("BasicPay_MasterId");
      entity.Property(e => e.BasicpayName).HasColumnName("Basicpay_Name");
      entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");
    });

    modelBuilder.Entity<TblBatch>(entity =>
    {
      entity.HasKey(e => e.BatchId).HasName("PK_dbo.Tbl_Batches");

      entity.ToTable("Tbl_Batches");

      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.BatchName).HasColumnName("Batch_Name");
    });

    modelBuilder.Entity<TblBloodGroup>(entity =>
    {
      entity.HasKey(e => e.BloodGroupId).HasName("PK_dbo.Tbl_BloodGroup");

      entity.ToTable("Tbl_BloodGroup");

      entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroup_Id");
      entity.Property(e => e.BloodGroup).HasColumnName("Blood_Group");
    });

    modelBuilder.Entity<TblCaste>(entity =>
    {
      entity.HasKey(e => e.CasteId).HasName("PK_dbo.Tbl_Caste");

      entity.ToTable("Tbl_Caste");

      entity.Property(e => e.CasteId).HasColumnName("Caste_Id");
      entity.Property(e => e.CasteName).HasColumnName("Caste_Name");
    });

    modelBuilder.Entity<TblCategory>(entity =>
    {
      entity.HasKey(e => e.CategoryId).HasName("PK_dbo.Tbl_Category");

      entity.ToTable("Tbl_Category");

      entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
      entity.Property(e => e.CategoryName).HasColumnName("Category_Name");
    });

    modelBuilder.Entity<TblClass>(entity =>
    {
      entity.HasKey(e => e.ClassId).HasName("PK_dbo.Tbl_Class");

      entity.ToTable("Tbl_Class");

      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
    });

    //modelBuilder.Entity<TblClassSubjects>(entity =>
    //{
    //  entity.ToTable("Tbl_ClassSubject");

    //  entity.Property(e => e.BoardId).HasColumnName("BoardID");
    //  entity.Property(e => e.ClassId).HasColumnName("ClassID");
    //  entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
    //});

    modelBuilder.Entity<TblClasssetup>(entity =>
    {
      entity.HasKey(e => e.ClassId).HasName("PK_dbo.Tbl_Classsetup");

      entity.ToTable("Tbl_Classsetup");

      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
    });

    modelBuilder.Entity<TblCoScholastic>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Tbl_CoSc__3214EC0745BE5BA9");

      entity.ToTable("Tbl_CoScholastic");

      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.Title).IsUnicode(false);
    });

    modelBuilder.Entity<TblCoScholasticClass>(entity =>
    {
      entity.ToTable("Tbl_CoScholasticClass");

      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.ClassId).HasColumnName("ClassID");
      entity.Property(e => e.ClassName).IsUnicode(false);
      entity.Property(e => e.CoscholasticId).HasColumnName("CoscholasticID");
    });

    modelBuilder.Entity<TblCoScholasticObtainedGrade>(entity =>
    {
      entity.ToTable("tbl_CoScholasticObtainedGrade");

      entity.Property(e => e.CoscholasticId).HasColumnName("CoscholasticID");
      entity.Property(e => e.ObtainedCoScholasticId).HasColumnName("ObtainedCoScholasticID");
      entity.Property(e => e.ObtainedGrade).IsUnicode(false);
    });

    modelBuilder.Entity<TblCoScholasticResult>(entity =>
    {
      entity.ToTable("Tbl_CoScholastic_Result");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.ClassId).HasColumnName("ClassID");
      entity.Property(e => e.CoScholasticId).HasColumnName("CoScholasticID");
      entity.Property(e => e.ObtainedGrade).IsUnicode(false);
      entity.Property(e => e.StudentId).HasColumnName("StudentID");
      entity.Property(e => e.TermId).HasColumnName("TermID");
    });

    modelBuilder.Entity<TblCommonDataListItem>(entity =>
    {
      entity.HasKey(e => e.DatalistId).HasName("PK_dbo.tbl_CommonDataListItem");

      entity.ToTable("tbl_CommonDataListItem");

      entity.Property(e => e.DataListItemName).HasMaxLength(500);
      entity.Property(e => e.DataListName).HasMaxLength(500);
      entity.Property(e => e.Spare1).HasMaxLength(35);
      entity.Property(e => e.Spare2).HasMaxLength(35);
      entity.Property(e => e.Spare3).HasMaxLength(35);
      entity.Property(e => e.Status).HasMaxLength(10);
    });

    modelBuilder.Entity<Tbl_CreateBank>(entity =>
    {
      entity.HasKey(e => e.Bank_Id).HasName("PK_dbo.Tbl_CreateBank");

      entity.ToTable("Tbl_CreateBank");

      entity.Property(e => e.Bank_Id).HasColumnName("Bank_Id");
      entity.Property(e => e.Bank_Code).HasColumnName("Bank_Code");
      entity.Property(e => e.Bank_Name).HasColumnName("Bank_Name");
      entity.Property(e => e.Contact_No).HasColumnName("Contact_No");
      entity.Property(e => e.Contactperson_Name).HasColumnName("Contactperson_Name");
    });

    modelBuilder.Entity<Tbl_CreateBranch>(entity =>
    {
      entity.HasKey(e => e.Branch_ID).HasName("PK_dbo.Tbl_CreateBranch");

      entity.ToTable("Tbl_CreateBranch");

      entity.Property(e => e.Branch_ID).HasColumnName("Branch_ID");
      entity.Property(e => e.Bank_Id).HasColumnName("Bank_Id");
      entity.Property(e => e.Bank_Name).HasColumnName("Bank_Name");
      entity.Property(e => e.Branch_Name).HasColumnName("Branch_Name");
      entity.Property(e => e.Contact_Name).HasColumnName("Contact_Name");
      entity.Property(e => e.Contact_No).HasColumnName("Contact_No");
      entity.Property(e => e.Landline_No).HasColumnName("Landline_No");
    });

    modelBuilder.Entity<Tbl_CreateMerchantId>(entity =>
    {
      entity.HasKey(e => e.Merchant_Id).HasName("PK_dbo.Tbl_CreateMerchantId");

      entity.ToTable("Tbl_CreateMerchantId");

      entity.Property(e => e.Merchant_Id).HasColumnName("Merchant_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Bank_Id).HasColumnName("Bank_Id");
      entity.Property(e => e.Branch_Id).HasColumnName("Branch_Id");
      entity.Property(e => e.IP).HasColumnName("IP");
      entity.Property(e => e.MerchantMID).HasColumnName("MerchantMID");
      entity.Property(e => e.MerchantName_Id).HasColumnName("MerchantName_Id");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.School_Id).HasColumnName("School_Id");
    });

    modelBuilder.Entity<TblCreateSchool>(entity =>
    {
      entity.HasKey(e => e.SchoolId).HasName("PK_dbo.TblCreateSchools");

      entity.Property(e => e.SchoolId).HasColumnName("School_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.SchoolName).HasColumnName("School_Name");
      entity.Property(e => e.UploadImage).HasColumnName("Upload_Image");
    });

    modelBuilder.Entity<TblDataList>(entity =>
    {
      entity.HasKey(e => e.DataListId).HasName("PK_dbo.Tbl_DataList");

      entity.ToTable("Tbl_DataList");
    });

    modelBuilder.Entity<TblDataListItem>(entity =>
    {
      entity.HasKey(e => e.DataListItemId).HasName("PK_dbo.Tbl_DataListItem");

      entity.ToTable("Tbl_DataListItem");
    });

    modelBuilder.Entity<TblDeclaration>(entity =>
    {
      entity.HasKey(e => e.DeclarationId).HasName("PK_dbo.tbl_Declaration");

      entity.ToTable("tbl_Declaration");

      entity.Property(e => e.Addeby).HasMaxLength(20);
      entity.Property(e => e.Addedon).HasMaxLength(20);
      entity.Property(e => e.Agree).HasMaxLength(10);
      entity.Property(e => e.Interesterd).HasMaxLength(10);
      entity.Property(e => e.NotInterested).HasMaxLength(512);
      entity.Property(e => e.Relocate).HasMaxLength(10);
      entity.Property(e => e.ScholarNumber).HasMaxLength(50);
      entity.Property(e => e.Spare1).HasMaxLength(35);
      entity.Property(e => e.Spare2).HasMaxLength(35);
      entity.Property(e => e.Spare3).HasMaxLength(35);
      entity.Property(e => e.StudentName).HasMaxLength(256);
      entity.Property(e => e.Updatedby).HasMaxLength(20);
      entity.Property(e => e.Updatedon).HasMaxLength(20);

      entity.HasOne(d => d.ScholarNumberNavigation).WithMany(p => p.TblDeclarations)
              .HasForeignKey(d => d.ScholarNumber)
              .HasConstraintName("FK_dbo.tbl_Declaration_dbo.tbl_StudentDetail_ScholarNumber");
    });

    modelBuilder.Entity<TblDeduction>(entity =>
    {
      entity.HasKey(e => e.DeductionsId).HasName("PK_dbo.Tbl_Deductions");

      entity.ToTable("Tbl_Deductions");

      entity.Property(e => e.DeductionsId).HasColumnName("Deductions_Id");
      entity.Property(e => e.AddedDate).HasColumnName("Added_Date");
      entity.Property(e => e.AddedDate1)
              .HasColumnType("datetime")
              .HasColumnName("AddedDate");
      entity.Property(e => e.AddedDay).HasColumnName("Added_Day");
      entity.Property(e => e.AddedMonth).HasColumnName("Added_Month");
      entity.Property(e => e.AddedYear).HasColumnName("Added_Year");
      entity.Property(e => e.DeductionAmt).HasColumnName("Deduction_Amt");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.NetPay).HasColumnName("Net_Pay");
      entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
      entity.Property(e => e.StaffName).HasColumnName("Staff_Name");
    });

    modelBuilder.Entity<TblDepartment>(entity =>
    {
      entity.HasKey(e => e.DepartmentId).HasName("PK_dbo.tbl_Department");

      entity.ToTable("tbl_Department");
    });

    modelBuilder.Entity<TblDueFee>(entity =>
    {
      entity.HasKey(e => e.DueFeeId).HasName("PK_dbo.TblDueFees");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TblEmailArchiefe>(entity =>
    {
      entity.HasKey(e => e.EmailId).HasName("PK_dbo.TblEmailArchieves");

      entity.Property(e => e.EmailId).HasColumnName("Email_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.EmailContent).HasColumnName("Email_Content");
      entity.Property(e => e.EmailDate).HasColumnName("Email_Date");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.ParentEmail).HasColumnName("Parent_Email");
      entity.Property(e => e.StudentId).HasColumnName("Student_id");
    });

    modelBuilder.Entity<TblEpfstatement>(entity =>
    {
      entity.HasKey(e => e.EpfstatementId).HasName("PK_dbo.Tbl_EPFStatement");

      entity.ToTable("Tbl_EPFStatement");

      entity.Property(e => e.EpfstatementId).HasColumnName("EPFstatement_Id");
      entity.Property(e => e.AddedDate).HasColumnName("Added_Date");
      entity.Property(e => e.AddedDay).HasColumnName("Added_Day");
      entity.Property(e => e.AddedMonth).HasColumnName("Added_Month");
      entity.Property(e => e.AddedYear).HasColumnName("Added_Year");
      entity.Property(e => e.Edliwages).HasColumnName("EDLIWages");
      entity.Property(e => e.EmployeContribution).HasColumnName("Employe_Contribution");
      entity.Property(e => e.EmployeeCode).HasColumnName("Employee_Code");
      entity.Property(e => e.EmployeeName).HasColumnName("Employee_Name");
      entity.Property(e => e.EmployerContribution).HasColumnName("Employer_Contribution");
      entity.Property(e => e.EpfWages).HasColumnName("Epf_Wages");
      entity.Property(e => e.EpsPension).HasColumnName("EPS_Pension");
      entity.Property(e => e.EpsWages).HasColumnName("EPs_Wages");
      entity.Property(e => e.GrossWages).HasColumnName("Gross_Wages");
      entity.Property(e => e.NcpDays).HasColumnName("NCP_Days");
      entity.Property(e => e.RefundAdvances).HasColumnName("Refund_Advances");
      entity.Property(e => e.StaffCategoryId).HasColumnName("StaffCategory_Id");
      entity.Property(e => e.Uin).HasColumnName("UIN");
    });

    modelBuilder.Entity<TblExamType>(entity =>
    {
      entity.HasKey(e => e.ExamId).HasName("PK_dbo.Tbl_ExamTypes");

      entity.ToTable("Tbl_ExamTypes");

      entity.Property(e => e.ExamId).HasColumnName("Exam_Id");
      entity.Property(e => e.ExamType).HasColumnName("Exam_Type");
    });

    modelBuilder.Entity<TblFeeReceipt>(entity =>
    {
      entity.HasKey(e => e.FeeReceiptId).HasName("PK_dbo.TblFeeReceipts");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.DueAmount).HasDefaultValueSql("(NULL)");
      entity.Property(e => e.FeeHeadingIds).HasColumnName("FeeHeadingIDs");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.PaidAmount).HasDefaultValueSql("(NULL)");
    });

    modelBuilder.Entity<TblLateFee>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.TblLateFees");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TblMenuName>(entity =>
    {
      entity.HasKey(e => e.MenuId).HasName("PK_dbo.Tbl_MenuName");

      entity.ToTable("Tbl_MenuName");

      entity.Property(e => e.MenuId).HasColumnName("Menu_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.MenuName).HasColumnName("Menu_Name");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.UploadImage).HasColumnName("Upload_Image");
    });

    modelBuilder.Entity<Tbl_MerchantName>(entity =>
    {
      entity.HasKey(e => e.MerchantName_Id).HasName("PK_dbo.Tbl_MerchantName");

      entity.ToTable("Tbl_MerchantName");

      entity.Property(e => e.MerchantName_Id).HasColumnName("MerchantName_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Bank_Id).HasColumnName("Bank_Id");
      entity.Property(e => e.Branch_Id).HasColumnName("Branch_Id");
      entity.Property(e => e.IP).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.School_Id).HasColumnName("School_Id");
    });

    modelBuilder.Entity<tbl_PaymentTransactionDetails>(entity =>
    {
      entity.HasKey(e => e.PaymentTransactionId).HasName("PK_dbo.tbl_PaymentTransactionDetails");

      entity.ToTable("tbl_PaymentTransactionDetails");

      entity.Property(e => e.Amount).HasMaxLength(20);
      entity.Property(e => e.Card).HasMaxLength(100);
      entity.Property(e => e.CardType).HasMaxLength(100);
      entity.Property(e => e.Member).HasMaxLength(100);
      entity.Property(e => e.PaymentId).HasMaxLength(100);
      entity.Property(e => e.Pmntmode).HasMaxLength(100);
      entity.Property(e => e.ReferenceNo).HasMaxLength(100);
      entity.Property(e => e.TrackId).HasMaxLength(100);
      entity.Property(e => e.TransactionError).HasMaxLength(1000);
      entity.Property(e => e.TransactionId).HasMaxLength(100);
      entity.Property(e => e.TransactionStatus).HasMaxLength(1000);
      entity.Property(e => e.TxnDate).HasMaxLength(30);
      entity.Property(e => e.Type).HasMaxLength(100);
    });

    modelBuilder.Entity<TblPaymentTransactionFeeDetail>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("tbl_PaymentTransactionFeeDetails");

      entity.Property(e => e.CreatedOn).HasColumnType("datetime");
      entity.Property(e => e.FeeAmount).HasMaxLength(100);
      entity.Property(e => e.FeeId).HasColumnName("FeeID");
      entity.Property(e => e.PaymentFeedetailsId)
              .ValueGeneratedOnAdd()
              .HasColumnName("PaymentFeedetailsID");
    });

    modelBuilder.Entity<TblPortion>(entity =>
    {
      entity.HasKey(e => e.PortionId).HasName("PK_dbo.Tbl_Portions");

      entity.ToTable("Tbl_Portions");

      entity.Property(e => e.PortionId).HasColumnName("Portion_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
      entity.Property(e => e.PortionDate).HasColumnName("Portion_Date");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.SectionName).HasColumnName("Section_Name");
      entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");
      entity.Property(e => e.SubjectName).HasColumnName("Subject_Name");
    });

    modelBuilder.Entity<TblReligion>(entity =>
    {
      entity.HasKey(e => e.ReligionId).HasName("PK_dbo.Tbl_Religion");

      entity.ToTable("Tbl_Religion");

      entity.Property(e => e.ReligionId).HasColumnName("Religion_Id");
      entity.Property(e => e.ReligionName).HasColumnName("Religion_Name");
    });

    modelBuilder.Entity<TblRemark>(entity =>
    {
      entity.HasKey(e => e.RemarkId);

      entity.ToTable("Tbl_Remark");

      entity.Property(e => e.Remark).IsUnicode(false);
    });

    modelBuilder.Entity<TblRevision>(entity =>
    {
      entity.HasKey(e => e.RevisionId).HasName("PK_dbo.Tbl_Revision");

      entity.ToTable("Tbl_Revision");

      entity.Property(e => e.RevisionId).HasColumnName("Revision_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
      entity.Property(e => e.RevisionDate).HasColumnName("Revision_Date");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.SectionName).HasColumnName("Section_Name");
      entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");
      entity.Property(e => e.SubjectName).HasColumnName("Subject_Name");
    });

    modelBuilder.Entity<TblRolePermissionNew>(entity =>
    {
      entity.HasKey(e => e.RolepermissionId).HasName("PK_dbo.Tbl_RolePermissionNew");

      entity.ToTable("Tbl_RolePermissionNew");

      entity.Property(e => e.RolepermissionId).HasColumnName("Rolepermission_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.CreatePermission).HasColumnName("Create_permission");
      entity.Property(e => e.DeletePermission).HasColumnName("Delete_Permission");
      entity.Property(e => e.EditPermission).HasColumnName("Edit_Permission");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.MenuId).HasColumnName("Menu_Id");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.RoleId).HasColumnName("Role_Id");
      entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
      entity.Property(e => e.StaffName).HasColumnName("Staff_Name");
      entity.Property(e => e.SubmenuId).HasColumnName("Submenu_Id");
      entity.Property(e => e.SubmenuName).HasColumnName("Submenu_Name");
      entity.Property(e => e.SubmenuPermission).HasColumnName("Submenu_permission");
      entity.Property(e => e.SubmenuUrl).HasColumnName("Submenu_Url");
      entity.Property(e => e.UpdatePermission).HasColumnName("Update_Permission");
    });

    modelBuilder.Entity<TblRoom>(entity =>
    {
      entity.HasKey(e => e.RoomId).HasName("PK_dbo.Tbl_Room");

      entity.ToTable("Tbl_Room");

      entity.Property(e => e.RoomId).HasColumnName("Room_Id");
      entity.Property(e => e.RoomName).HasColumnName("Room_Name");
      entity.Property(e => e.RoomNo).HasColumnName("Room_No");
      entity.Property(e => e.RoomType).HasColumnName("Room_Type");
      entity.Property(e => e.RoomTypeId).HasColumnName("RoomType_ID");
      entity.Property(e => e.SeatingCapacity).HasColumnName("Seating_Capacity");
    });

    modelBuilder.Entity<TblRoomType>(entity =>
    {
      entity.HasKey(e => e.RoomId).HasName("PK_dbo.Tbl_RoomType");

      entity.ToTable("Tbl_RoomType");

      entity.Property(e => e.RoomId).HasColumnName("Room_Id");
      entity.Property(e => e.RoomType).HasColumnName("Room_Type");
    });

    modelBuilder.Entity<TblSalaryStatement>(entity =>
    {
      entity.HasKey(e => e.SalaryStatementId).HasName("PK_dbo.Tbl_SalaryStatement");

      entity.ToTable("Tbl_SalaryStatement");

      entity.Property(e => e.SalaryStatementId).HasColumnName("SalaryStatement_Id");
      entity.Property(e => e.AccountDetails).HasColumnName("Account_Details");
      entity.Property(e => e.AccountDetailsId).HasColumnName("AccountDetails_Id");
      entity.Property(e => e.EmployeeAccountNo).HasColumnName("Employee_AccountNo");
      entity.Property(e => e.EmployeeCode).HasColumnName("Employee_Code");
      entity.Property(e => e.EmployeeName).HasColumnName("Employee_Name");
      entity.Property(e => e.EmployersDesignation).HasColumnName("Employers_Designation");
      entity.Property(e => e.SalaryStatementDate).HasColumnName("SalaryStatement_Date");
      entity.Property(e => e.SalarystatementMonth).HasColumnName("Salarystatement_Month");
      entity.Property(e => e.SalarystatementYear).HasColumnName("Salarystatement_year");
      entity.Property(e => e.StaffCategoryId).HasColumnName("StaffCategory_Id");
      entity.Property(e => e.TotalSalary).HasColumnName("Total_Salary");
    });

    modelBuilder.Entity<Tbl_SchoolSetup>(entity =>
    {
      entity.HasKey(e => e.Schoolsetup_Id).HasName("PK_dbo.Tbl_SchoolSetup");

      entity.ToTable("Tbl_SchoolSetup");

      entity.Property(e => e.Schoolsetup_Id).HasColumnName("Schoolsetup_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Bank_Id).HasColumnName("Bank_Id");
      entity.Property(e => e.Branch_Id).HasColumnName("Branch_Id");
      entity.Property(e => e.Fee_Configuratinname).HasColumnName("Fee_Configuratinname");
      entity.Property(e => e.Fee_configurationId).HasColumnName("Fee_configurationId");
      entity.Property(e => e.IP).HasColumnName("IP");
      entity.Property(e => e.Merchant_nameId).HasColumnName("Merchant_nameId");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.School_Id).HasColumnName("School_Id");
    });

    modelBuilder.Entity<TblSectionSetup>(entity =>
    {
      entity.HasKey(e => e.SectionId).HasName("PK_dbo.Tbl_SectionSetup");

      entity.ToTable("Tbl_SectionSetup");

      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
    });

    modelBuilder.Entity<TblSemester>(entity =>
    {
      entity.HasKey(e => e.SemesterId).HasName("PK_dbo.tbl_Semester");

      entity.ToTable("tbl_Semester");

      entity.Property(e => e.Addeby).HasMaxLength(20);
      entity.Property(e => e.Addedon).HasMaxLength(20);
      entity.Property(e => e.Percentage).HasMaxLength(50);
      entity.Property(e => e.Perse2)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("perse2");
      entity.Property(e => e.Persentagegrade).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ScholarNumber).HasMaxLength(50);
      entity.Property(e => e.Sem).HasMaxLength(20);
      entity.Property(e => e.Spare1).HasMaxLength(35);
      entity.Property(e => e.Spare2).HasMaxLength(35);
      entity.Property(e => e.Spare3).HasMaxLength(35);
      entity.Property(e => e.Updatedby).HasMaxLength(20);
      entity.Property(e => e.Updatedon).HasMaxLength(20);
      entity.Property(e => e.Year).HasMaxLength(20);
    });

    modelBuilder.Entity<TblSetTime>(entity =>
    {
      entity.HasKey(e => e.TimeId).HasName("PK_dbo.Tbl_SetTime");

      entity.ToTable("Tbl_SetTime");

      entity.Property(e => e.TimeId).HasColumnName("Time_Id");
    });

    modelBuilder.Entity<TblSibling>(entity =>
    {
      entity.HasKey(e => e.SiblingsId).HasName("PK_dbo.Tbl_Siblings");

      entity.ToTable("Tbl_Siblings");

      entity.Property(e => e.SiblingsId).HasColumnName("Siblings_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ClassId).HasColumnName("Class_id");
      entity.Property(e => e.FamilyDetailsId).HasColumnName("FamilyDetails_Id");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.StudentId).HasColumnName("Student_Id");
    });

    modelBuilder.Entity<TblSkillset>(entity =>
    {
      entity.HasKey(e => e.SkillsetId).HasName("PK_dbo.tbl_skillset");

      entity.ToTable("tbl_skillset");

      entity.Property(e => e.Addedby).HasMaxLength(20);
      entity.Property(e => e.Addedon).HasMaxLength(20);
      entity.Property(e => e.Updatedby).HasMaxLength(20);
      entity.Property(e => e.Updatedon).HasMaxLength(20);
    });

    //modelBuilder.Entity<TblStaffAttendance>(entity =>
    //{
    //  entity.HasKey(e => e.StaffAtteId).HasName("PK_dbo.Tbl_StaffAttendance");

    //  entity.ToTable("Tbl_StaffAttendance");

    //  entity.Property(e => e.StaffAtteId).HasColumnName("StaffAtte_Id");
    //  entity.Property(e => e.AddedDate).HasColumnType("datetime");
    //  entity.Property(e => e.AttendenceDate).HasColumnName("Attendence_Date");
    //  entity.Property(e => e.AttendenceDay).HasColumnName("Attendence_Day");
    //  entity.Property(e => e.AttendenceMonth).HasColumnName("Attendence_Month");
    //  entity.Property(e => e.AttendenceYear).HasColumnName("Attendence_Year");
    //  entity.Property(e => e.EmployeeCode).HasColumnName("Employee_Code");
    //  entity.Property(e => e.Ip).HasColumnName("IP");
    //  entity.Property(e => e.MarkFullDayAbsent).HasColumnName("Mark_FullDayAbsent");
    //  entity.Property(e => e.MarkFullDayPresent).HasColumnName("Mark_FullDayPresent");
    //  entity.Property(e => e.MarkHalfDayAbsent).HasColumnName("Mark_HalfDayAbsent");
    //  entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    //  entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
    //  entity.Property(e => e.StaffName).HasColumnName("Staff_Name");
    //});

    modelBuilder.Entity<TblStaffCategory>(entity =>
    {
      entity.HasKey(e => e.StaffCategoryId).HasName("PK_dbo.Tbl_StaffCategory");

      entity.ToTable("Tbl_StaffCategory");

      entity.Property(e => e.StaffCategoryId).HasColumnName("Staff_Category_Id");
      entity.Property(e => e.CategoryName).HasColumnName("Category_Name");
      entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");
    });

    modelBuilder.Entity<TblStaffSalary>(entity =>
    {
      entity.HasKey(e => e.SalaryId).HasName("PK_dbo.Tbl_StaffSalary");

      entity.ToTable("Tbl_StaffSalary");

      entity.Property(e => e.SalaryId).HasColumnName("Salary_Id");
      entity.Property(e => e.BasicAmount).HasColumnName("Basic_Amount");
      entity.Property(e => e.SalaryAmount).HasColumnName("Salary_Amount");
      entity.Property(e => e.StaffId).HasColumnName("Staff_ID");
      entity.Property(e => e.StaffName).HasColumnName("Staff_Name");
    });

    modelBuilder.Entity<TblStudentAttendance>(entity =>
    {
      entity.HasKey(e => e.AttendanceId).HasName("PK_dbo.Tbl_StudentAttendance");

      entity.ToTable("Tbl_StudentAttendance");

      entity.Property(e => e.AttendanceId).HasColumnName("Attendance_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
      entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
      entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");
      entity.Property(e => e.MarkFullDayAbsent).HasColumnName("Mark_FullDayAbsent");
      entity.Property(e => e.MarkHalfDayAbsent).HasColumnName("Mark_HalfDayAbsent");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.SectionName).HasColumnName("Section_Name");
      entity.Property(e => e.StudentName).HasColumnName("Student_Name");
      entity.Property(e => e.StudentRegisterId).HasColumnName("StudentRegisterID");
    });

    modelBuilder.Entity<TblStudentDetail>(entity =>
    {
      entity.HasKey(e => e.ScholarNumber).HasName("PK_dbo.tbl_StudentDetail");

      entity.ToTable("tbl_StudentDetail");

      entity.Property(e => e.ScholarNumber).HasMaxLength(50);
      entity.Property(e => e.Addeby).HasMaxLength(20);
      entity.Property(e => e.Addedon).HasMaxLength(20);
      entity.Property(e => e.Age).HasMaxLength(5);
      entity.Property(e => e.Batch).HasMaxLength(20);
      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.Category).HasMaxLength(256);
      entity.Property(e => e.Class).HasMaxLength(50);
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.Cmcremarks)
              .HasMaxLength(256)
              .HasColumnName("CMCRemarks");
      entity.Property(e => e.CorrespondenceAddress).HasMaxLength(512);
      entity.Property(e => e.CountryCode).HasMaxLength(10);
      entity.Property(e => e.Course).HasMaxLength(256);
      entity.Property(e => e.DateOn).HasMaxLength(50);
      entity.Property(e => e.DateofBirth).HasMaxLength(256);
      entity.Property(e => e.EmailId).HasMaxLength(256);
      entity.Property(e => e.FacultyMentor).HasMaxLength(256);
      entity.Property(e => e.FatherCompanyName).HasMaxLength(256);
      entity.Property(e => e.FatherCountryCode).HasMaxLength(10);
      entity.Property(e => e.FatherEmailId).HasMaxLength(256);
      entity.Property(e => e.FatherMobileNo).HasMaxLength(256);
      entity.Property(e => e.FatherName).HasMaxLength(256);
      entity.Property(e => e.FatherProfession).HasMaxLength(256);
      entity.Property(e => e.Gender).HasMaxLength(256);
      entity.Property(e => e.Hostalite).HasMaxLength(256);
      entity.Property(e => e.MobileNo).HasMaxLength(20);
      entity.Property(e => e.MotherCompanyName).HasMaxLength(256);
      entity.Property(e => e.MotherCountryCode).HasMaxLength(10);
      entity.Property(e => e.MotherEmailId).HasMaxLength(256);
      entity.Property(e => e.MotherMobileNo).HasMaxLength(256);
      entity.Property(e => e.MotherName).HasMaxLength(256);
      entity.Property(e => e.MotherProfession).HasMaxLength(256);
      entity.Property(e => e.NativePlace).HasMaxLength(256);
      entity.Property(e => e.OutStationStudent).HasMaxLength(256);
      entity.Property(e => e.Religious).HasMaxLength(50);
      entity.Property(e => e.ReligiousOther).HasMaxLength(50);
      entity.Property(e => e.ResidenceLocation).HasMaxLength(256);
      entity.Property(e => e.Sibiling1).HasMaxLength(256);
      entity.Property(e => e.Sibiling2).HasMaxLength(256);
      entity.Property(e => e.Sibiling3).HasMaxLength(256);
      entity.Property(e => e.Sibiling4).HasMaxLength(256);
      entity.Property(e => e.Sibiling5).HasMaxLength(256);
      entity.Property(e => e.Spare1).HasMaxLength(35);
      entity.Property(e => e.Spare2).HasMaxLength(35);
      entity.Property(e => e.Spare3).HasMaxLength(35);
      entity.Property(e => e.Specialization).HasMaxLength(256);
      entity.Property(e => e.Status)
              .HasMaxLength(20)
              .HasColumnName("status");
      entity.Property(e => e.StudentId).ValueGeneratedOnAdd();
      entity.Property(e => e.StudentName).HasMaxLength(256);
      entity.Property(e => e.Updatedby).HasMaxLength(20);
      entity.Property(e => e.Updatedon).HasMaxLength(20);
      entity.Property(e => e.Years).HasMaxLength(20);
    });

    modelBuilder.Entity<TblStudentElectiveRecord>(entity =>
    {
      entity.ToTable("Tbl_Student_ElectiveRecord");
    });

    modelBuilder.Entity<TblStudentFeeSaved>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.TblStudentFeeSaveds");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TblStudentPromote>(entity =>
    {
      entity.HasKey(e => e.PromoteId).HasName("PK_dbo.Tbl_StudentPromote");

      entity.ToTable("Tbl_StudentPromote");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.FromClassId).HasColumnName("FromClass_Id");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.RegistrationDate).HasColumnName("Registration_Date");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.StudentId).HasColumnName("Student_Id");
      entity.Property(e => e.ToClassId).HasColumnName("ToClass_Id");
      entity.Property(e => e.ToSection)
              .HasMaxLength(255)
              .IsUnicode(false);
    });

    modelBuilder.Entity<TblSubjectsSetup>(entity =>
    {
      entity.HasKey(e => e.SubjectId).HasName("PK_dbo.Tbl_SubjectsSetup");

      entity.ToTable("Tbl_SubjectsSetup");

      entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");
      entity.Property(e => e.SubjectName).HasColumnName("Subject_Name");
    });

    modelBuilder.Entity<TblSubmenuName>(entity =>
    {
      entity.HasKey(e => e.SubmenuId).HasName("PK_dbo.Tbl_SubmenuName");

      entity.ToTable("Tbl_SubmenuName");

      entity.Property(e => e.SubmenuId).HasColumnName("Submenu_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.MenuId).HasColumnName("Menu_Id");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.SubmenuName).HasColumnName("Submenu_Name");
      entity.Property(e => e.SubmenuPermission).HasColumnName("Submenu_permission");
      entity.Property(e => e.SubmenuUrl).HasColumnName("Submenu_Url");
    });

    modelBuilder.Entity<TblSummerInternship>(entity =>
    {
      entity.HasKey(e => e.SummerInternshipId).HasName("PK_dbo.tbl_SummerInternship");

      entity.ToTable("tbl_SummerInternship");

      entity.Property(e => e.Addeby).HasMaxLength(20);
      entity.Property(e => e.Addedon).HasMaxLength(20);
      entity.Property(e => e.CompanyName).HasMaxLength(512);
      entity.Property(e => e.EndDate).HasMaxLength(20);
      entity.Property(e => e.FacultyGuideMobileNo).HasMaxLength(20);
      entity.Property(e => e.FacultyProjectGuide).HasMaxLength(256);
      entity.Property(e => e.Feedback).HasMaxLength(512);
      entity.Property(e => e.IndustryGuideDesignation).HasMaxLength(256);
      entity.Property(e => e.IndustryGuideEmail).HasMaxLength(512);
      entity.Property(e => e.IndustryGuideMobileNo).HasMaxLength(20);
      entity.Property(e => e.IndustryGuideName).HasMaxLength(256);
      entity.Property(e => e.IndustryGuideTelNo).HasMaxLength(20);
      entity.Property(e => e.MobileNo).HasMaxLength(20);
      entity.Property(e => e.PrePlacementOfferReceived).HasMaxLength(10);
      entity.Property(e => e.ProjectDescription).HasMaxLength(512);
      entity.Property(e => e.ProjectSubmission).HasMaxLength(10);
      entity.Property(e => e.ProjectTitle).HasMaxLength(256);
      entity.Property(e => e.ReasonforNoSubmission).HasMaxLength(512);
      entity.Property(e => e.ScholarNumber).HasMaxLength(50);
      entity.Property(e => e.Spare1).HasMaxLength(35);
      entity.Property(e => e.Spare2).HasMaxLength(35);
      entity.Property(e => e.Spare3).HasMaxLength(35);
      entity.Property(e => e.StartDate).HasMaxLength(20);
      entity.Property(e => e.StipendinThousands).HasMaxLength(20);
      entity.Property(e => e.Updatedby).HasMaxLength(20);
      entity.Property(e => e.Updatedon).HasMaxLength(20);

      entity.HasOne(d => d.ScholarNumberNavigation).WithMany(p => p.TblSummerInternships)
              .HasForeignKey(d => d.ScholarNumber)
              .HasConstraintName("FK_dbo.tbl_SummerInternship_dbo.tbl_StudentDetail_ScholarNumber");
    });

    modelBuilder.Entity<TblTcAmount>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.Tbl_TcAmount");

      entity.ToTable("Tbl_TcAmount");

      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<TblTeacherAllocation>(entity =>
    {
      entity.HasKey(e => e.AllocateId).HasName("PK_dbo.Tbl_TeacherAllocation");

      entity.ToTable("Tbl_TeacherAllocation");

      entity.Property(e => e.AllocateId).HasColumnName("Allocate_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
      entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");
      entity.Property(e => e.SubjectName).HasColumnName("Subject_Name");
      entity.Property(e => e.TeacherName).HasColumnName("Teacher_Name");
    });

    modelBuilder.Entity<TblTerm>(entity =>
    {
      entity.HasKey(e => e.TermId);

      entity.ToTable("Tbl_Term");

      entity.Property(e => e.TermId).HasColumnName("TermID");
      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.TermName)
              .HasMaxLength(200)
              .IsUnicode(false);
    });

    modelBuilder.Entity<TblTests >(entity =>
    {
      entity.HasKey(e => e.TestId);

      entity.ToTable("Tbl_Tests");

      entity.Property(e => e.TestId).HasColumnName("TestID");
      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.ClassId).HasColumnName("ClassID");
      entity.Property(e => e.MaximumMarks).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
      entity.Property(e => e.TermId).HasColumnName("TermID");
      entity.Property(e => e.TestName)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.TestType)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<TblTestAssignDates>(entity =>
    {
      entity.HasKey(e => e.TestAssignId);

      entity.ToTable("TblTestAssignDates");

      entity.Property(e => e.TestAssignId).HasColumnName("TestAssignID");
      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.CreatedAt).HasColumnType("datetime");
      entity.Property(e => e.TestId).HasColumnName("TestID");
      entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
    });

    modelBuilder.Entity<TblTestObtainedMark>(entity =>
    {
      entity.ToTable("Tbl_TestObtainedMark");

      entity.Property(e => e.ObtainedMarks).HasColumnType("decimal(18, 1)");
      entity.Property(e => e.RecordIdfk).HasColumnName("RecordIDFK");
      entity.Property(e => e.TestId).HasColumnName("TestID");
    });

    modelBuilder.Entity<TblTestRecord>(entity =>
    {
      entity.HasKey(e => e.RecordId);

      entity.ToTable("Tbl_TestRecords");

      entity.Property(e => e.RecordId).HasColumnName("RecordID");
      entity.Property(e => e.BoardId).HasColumnName("BoardID");
      entity.Property(e => e.ClassId).HasColumnName("ClassID");
      entity.Property(e => e.ObtainedMarks).HasColumnType("numeric(18, 1)");
      entity.Property(e => e.SectionId).HasColumnName("SectionID");
      entity.Property(e => e.StudentId).HasColumnName("StudentID");
      entity.Property(e => e.TermId).HasColumnName("TermID");
      entity.Property(e => e.TestId).HasColumnName("TestID");
    });

    modelBuilder.Entity<TblTimeTable>(entity =>
    {
      entity.HasKey(e => e.TimeTableId).HasName("PK_dbo.Tbl_TimeTable");

      entity.ToTable("Tbl_TimeTable");

      entity.Property(e => e.TimeTableId).HasColumnName("TimeTable_Id");
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.ClassName).HasColumnName("Class_Name");
      entity.Property(e => e.DayId).HasColumnName("Day_ID");
      entity.Property(e => e.DayTimeId).HasColumnName("Day_Time_Id");
      entity.Property(e => e.RoomId).HasColumnName("Room_Id");
      entity.Property(e => e.RoomName).HasColumnName("Room_Name");
      entity.Property(e => e.SectionId).HasColumnName("Section_Id");
      entity.Property(e => e.SectionName).HasColumnName("Section_Name");
      entity.Property(e => e.StaffName).HasColumnName("Staff_Name");
      entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");
      entity.Property(e => e.SubjectName).HasColumnName("Subject_Name");
      entity.Property(e => e.TimeId).HasColumnName("Time_ID");
    });

    modelBuilder.Entity<TblTransportFeeReceipt>(entity =>
    {
      entity.HasKey(e => e.FeeReceiptId).HasName("PK_dbo.TblTransportFeeReceipts");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.DueAmount).HasDefaultValueSql("(NULL)");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.PaidAmount).HasDefaultValueSql("(NULL)");
    });

    modelBuilder.Entity<TblTransportKm>(entity =>
    {
      entity.HasKey(e => e.KmId).HasName("PK_dbo.Tbl_TransportKm");

      entity.ToTable("Tbl_TransportKm");

      entity.Property(e => e.KmId).HasColumnName("Km_Id");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Amount).HasColumnName("AMount");
      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.FromKm).HasColumnName("From_Km");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.SchoolId).HasColumnName("School_Id");
      entity.Property(e => e.ToKm).HasColumnName("To_Km");
    });

    modelBuilder.Entity<TblTransportReducedAmount>(entity =>
    {
      entity.HasKey(e => e.ReducedAmountId).HasName("PK_dbo.TblTransportReducedAmounts");

      entity.Property(e => e.ReducedAmountId).HasColumnName("ReducedAmount_Id");
    });

    modelBuilder.Entity<TblUserDynamicConfiguration>(entity =>
    {
      entity.HasKey(e => e.Mainid).HasName("PK_dbo.TblUserDynamicConfigurations");
    });

    modelBuilder.Entity<TblUserManagement>(entity =>
    {
      entity.HasKey(e => e.UserId).HasName("PK_dbo.Tbl_UserManagement");

      entity.ToTable("Tbl_UserManagement");
    });

    modelBuilder.Entity<TblWeekDay>(entity =>
    {
      entity.HasKey(e => e.DayId).HasName("PK_dbo.Tbl_WeekDays");

      entity.ToTable("Tbl_WeekDays");

      entity.Property(e => e.DayId).HasColumnName("Day_Id");
      entity.Property(e => e.WeekDay).HasColumnName("Week_day");
    });

    modelBuilder.Entity<TblWorkExperience>(entity =>
    {
      entity.HasKey(e => e.WorkExperienceId).HasName("PK_dbo.tbl_WorkExperience");

      entity.ToTable("tbl_WorkExperience");

      entity.Property(e => e.Addeby).HasMaxLength(20);
      entity.Property(e => e.Addedon).HasMaxLength(20);
      entity.Property(e => e.CompanyName).HasMaxLength(512);
      entity.Property(e => e.CompanyProfile).HasMaxLength(512);
      entity.Property(e => e.Designation).HasMaxLength(256);
      entity.Property(e => e.ScholarNumber).HasMaxLength(50);
      entity.Property(e => e.Spare1).HasMaxLength(35);
      entity.Property(e => e.Spare2).HasMaxLength(35);
      entity.Property(e => e.Spare3).HasMaxLength(35);
      entity.Property(e => e.ToDate).HasMaxLength(20);
      entity.Property(e => e.TotalExperience).HasMaxLength(5);
      entity.Property(e => e.Updatedby).HasMaxLength(20);
      entity.Property(e => e.Updatedon).HasMaxLength(20);
    });

    modelBuilder.Entity<TcFeeDetail>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.TcFeeDetails");

      entity.Property(e => e.CreatedOn).HasColumnType("datetime");
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.ReceiptAmount).HasColumnType("decimal(18, 2)");

      entity.HasOne(d => d.Student).WithMany(p => p.TcFeeDetails)
              .HasForeignKey(d => d.StudentId)
              .HasConstraintName("FK_dbo.TcFeeDetails_dbo.Students_StudentId");

      entity.HasOne(d => d.StudentTcDetails).WithMany(p => p.TcFeeDetails)
              .HasForeignKey(d => d.StudentTcDetailsId)
              .HasConstraintName("FK_dbo.TcFeeDetails_dbo.StudentTcDetails_StudentTcDetailsId");
    });

    modelBuilder.Entity<TermClassMapping>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__TermClas__3214EC072A6B46EF");

      entity.ToTable("TermClassMapping");
    });

    modelBuilder.Entity<TimeSetting>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_dbo.TimeSettings");
    });

    modelBuilder.Entity<TransportFeeConfiguration>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
      entity.Property(e => e.BatchName).HasMaxLength(100);
      entity.Property(e => e.Class).HasMaxLength(100);
      entity.Property(e => e.ClassId).HasColumnName("Class_Id");
      entity.Property(e => e.CreatedOn).HasColumnType("datetime");
      entity.Property(e => e.FromKm).HasColumnName("FromKM");
      entity.Property(e => e.ToKm).HasColumnName("ToKM");
      entity.Property(e => e.TransportFeeConfigurationId)
              .ValueGeneratedOnAdd()
              .HasColumnName("TransportFeeConfigurationID");
    });

    modelBuilder.Entity<TransportFeeHeading>(entity =>
    {
      entity.HasKey(e => e.TransportFeeId).HasName("PK_dbo.TransportFeeHeadings");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TransportFeePlan>(entity =>
    {
      entity.HasKey(e => e.FeePlanId).HasName("PK_dbo.TransportFeePlans");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Ip).HasColumnName("IP");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
    });
    modelBuilder.Entity<Tbl_PublishDetail>(entity =>
    {
      entity.HasKey(e => e.PublishId).HasName("PK_dbo.PublishId");

    });
    modelBuilder.Entity<Tbl_FreezeData>(entity =>
    {
      entity.HasKey(e => e.FreezeId).HasName("PK_dbo.FreezeId");

    });
    modelBuilder.Entity<tbl_PaymentTransactionDetails>(entity =>
    {
      entity.HasKey(e => e.PaymentTransactionId).HasName("PK_dbo.PaymentTransactionId");

    });

    modelBuilder.Entity<Tbl_MerchantName>(entity =>
    {
      entity.HasKey(e => e.MerchantName_Id).HasName("PK_dbo.MerchantName_Id");

    });
    //modelBuilder.Entity<Tbl_CreateMerchantId>(entity =>
    //{
    //  entity.HasKey(e => e.Merchant_Id).HasName("PK_dbo.Merchant_Id");

    //});
    modelBuilder.Entity<Tbl_SchoolSetup>(entity =>
    {
      entity.HasKey(e => e.Schoolsetup_Id).HasName("PK_dbo.Schoolsetup_Id");

    });

    modelBuilder.Entity<Tbl_CreateBank>(entity =>
    {
      entity.HasKey(e => e.Bank_Id).HasName("PK_dbo.Bank_Id");

    });
    modelBuilder.Entity<Tbl_CreateBranch>(entity =>
    {
      entity.HasKey(e => e.Branch_ID).HasName("PK_dbo.Branch_ID");

    });
    

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
