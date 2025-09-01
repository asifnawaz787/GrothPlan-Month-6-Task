namespace GrothPlan_Month_6_Task.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UploadFile
    {
        public string FileName { get; set; }
        public IFormFile Upload { get; set; }
        public string EmployeeEmail {  get; set; }
    }

    public class DocumentEditModel
    {
        public string DocumentName { get; set; }
        public string EmployeeEmail { get; set; }
    }

    public class AddEmployee
    {
        public string Name { get; set; }
        public string Email {  set; get; }
        public string Password { set; get; }
        public string Phone {  set; get; }
        public string RoleName {  get; set; }
        public IFormFile Upload {  set; get; }
        public string DocumentName { set; get; }
    }

}
