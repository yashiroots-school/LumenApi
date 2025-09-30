namespace LumenApi.Web.Models.StudentView;

public class StudnetsDetailsView
{
  public long SerialNumber { get; set; } = 0;
  public long StudentID { get; set; } = 0;
  public string ScholarNo { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Last_Name { get; set; } = string.Empty;
  public string School { get; set; } = string.Empty;
  public string FatherName { get; set; } = string.Empty;
  public string MotherName { get; set; } = string.Empty;
  public string FMobile { get; set; } = string.Empty;
  public string FResidentialAddress { get; set; } = string.Empty;
  public string AdharNo { get; set; } = string.Empty;
  public string FEMail { get; set; } = string.Empty;
  public string Class { get; set; } = string.Empty;
  public string Section { get; set; } = string.Empty;
  public string CastName { get; set; } = string.Empty;
  public string Category { get; set; } = string.Empty;
  public string Religion { get; set; } = string.Empty;
  public string Gender { get; set; } = string.Empty;
  public string DOB { get; set; } = string.Empty;
  public long CurrentYear { get; set; } = 0;
  public string ParentEmail { get; set; } = string.Empty;
  public string BloodGroup { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string STATE { get; set; } = string.Empty;
  public string Pincode { get; set; } = string.Empty;
  public string AdmissionDate { get; set; } = string.Empty;
  public string Promotion_Date { get; set; } = string.Empty;
  public string SSSMIdNumber { get; set; } = string.Empty;
  public string BankAccount { get; set; } = string.Empty;
  public string BankName { get; set; } = string.Empty;
  public string BankACHolder { get; set; } = string.Empty;
  public string BankIFSC { get; set; } = string.Empty;
  public string Subjects { get; set; } = string.Empty;
  public string OptionalSubjects { get; set; } = string.Empty;
  public string ApplicationNumber { get; set; } = string.Empty;
  public string ApaarId { get; set; } = string.Empty;
  public string PerEduNumber { get; set; } = string.Empty;
  public long TotalRecords { get; set; } = 0;
  public decimal TotalDaysPresent { get; set; } = 0;
  public decimal PaidAmount { get; set; } = 0;
  public decimal Percentage { get; set; } = 0;
  public string ProfilePicture { get; set; } = string.Empty;
  public int ClassId {  get; set; } = 0;
  public int SectionId { get; set; } = 0;
  public int BatchId { get; set; }
}

public class CoScholasticResult
{
  public long StudentID { get; set; }
  public int TermID { get; set; }
  public long BatchId { get; set; }
  public string Batch_Name { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string ObtainedGrade { get; set; } = string.Empty;
}

public class StudentAttendanceSummary
{
  public long StudentRegisterId { get; set; }
  public long BatchId { get; set; }
  public int TotalDays { get; set; }
  public decimal PresentDays { get; set; }
  public decimal AttendancePercent { get; set; }
  public string Batch_Name { get; set; } = string.Empty;
  // Add more fields as per view definition
}

public class StudentTestPercentage
{
  public long StudentId { get; set; }
  public int TermId { get; set; }
  public decimal Percentage { get; set; }
  public string Grade { get; set; } = string.Empty;
  public long BatchId { get; set; }
  public string Batch_Name { get; set; } = string.Empty;
  // Add more fields as per view definition
}
public class StudentFeeDetails
{
  public decimal PaidAmount { get; set; }
  public decimal DueAmount { get; set; }
  
  // Add more fields as per view definition
}

public class StudentProfileSummaryBatchWise
{
  public List<CoScholasticResult> CoScholasticResults { get; set; } = new();
  public List<StudentAttendanceSummary> AttendanceSummaries { get; set; } = new();
  public List<StudentTestPercentage> TestPercentages { get; set; } = new();
  public List<StudentFeeDetails> feeDetails { get; set; } = new();
}
public class TimeTableView
{
  public string DayName { get; set; } = string.Empty;
  public string Class { get; set; } = string.Empty;
  public string Section { get; set; } = string.Empty;
  public string Subject { get; set; } = string.Empty;
  public string StaffName { get; set; } = string.Empty;
  public string Period { get; set; } = string.Empty;
  public string LoadPer { get; set;} = string.Empty;
}
public class StaffDetailsModel
{
  public long StafId { get; set; }
  public string UIN { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Gender { get; set; } = string.Empty;
  public string AgeInWords { get; set; } = string.Empty;
  public string DOB { get; set; }= string.Empty;
  public string POB { get; set; } = string.Empty;
  public string Nationality { get; set; } = string.Empty;
  public string Religion { get; set; } = string.Empty;
  public string Qualification { get; set; } = string.Empty;
  public string WorkExperience { get; set; } = string.Empty;
  public string MotherTongue { get; set; } = string.Empty;
  public string Contact { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string EmpId { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public string Designation { get; set; } = string.Empty;
  public string FatherOrHusbandName { get; set; } = string.Empty;
  public string MariedStatus { get; set; } = string.Empty;
  public string MothersName { get; set; } = string.Empty;
  public string Children { get; set; } = string.Empty;
  public string StaffSignatureFile { get; set; } = string.Empty;
  public string Employee_Designation { get; set; } = string.Empty;
  public string Staff_CategoryName { get; set; } = string.Empty;
  public string UAN { get; set; } = string.Empty;
  public string StaffCategory { get; set; } = string.Empty;
}

