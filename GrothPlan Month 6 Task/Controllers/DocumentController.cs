using GrothPlan_Month_6_Task.Data;
using GrothPlan_Month_6_Task.Entities;
using GrothPlan_Month_6_Task.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrothPlan_Month_6_Task.Controllers
{
    public class DocumentController : Controller
    {
        private readonly AppDbContext _context;
       
        public DocumentController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [Consumes("multipart/form-data")]
        public IActionResult UploadDocument([FromForm]  UploadFile viewModel)
        {
            var extension = Path.GetExtension(viewModel.Upload.FileName);
            if (extension == ".pdf" || extension == ".xml")
            {
                var fileName = "wwwroot/Documents/" + viewModel.FileName + Path.GetExtension(viewModel.Upload.FileName);
                if (!Directory.Exists("wwwroot/Documents"))
                {
                    Directory.CreateDirectory("wwwroot/Documents");
                }
                using (var stream = new FileStream(fileName, FileMode.Create))
                {

                    viewModel.Upload.CopyTo(stream);
                }
                var employee = _context.Employees.FirstOrDefault(e => e.Email == viewModel.EmployeeEmail);
                if (employee != null)
                {
                    _context.Documents.Add(new Entities.Document() { EmployeeId = employee.EmployeeId, FileName = viewModel.FileName });
                    _context.SaveChanges();
                    return Ok("File is uploaded successfully");
                }
            }
            else
            {
                return Ok("only pdf and xml files are allowed to upload");
            }
            return Ok("File uploaded successfully.");
        }
        public IActionResult DocumentChanged()
        {
            return View();
        }
        public IActionResult ChangeDocument(DocumentEditModel viewModel)
        {
            var employee = _context.Employees.FirstOrDefault(e=>e.Email==viewModel.EmployeeEmail);
            var role = _context.Roles.FirstOrDefault(e=>e.EmployeeId==employee.EmployeeId);
            //var documents = _context.Documents.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId && e.FileName==viewModel.DocumentName);
            if (employee.Roles.FirstOrDefault().RoleName == "Admin")
            {
              var document=  _context.Documents.FirstOrDefault(e=>e.FileName==viewModel.DocumentName);
                document.EmployeeId=employee.EmployeeId;
                _context.SaveChanges();
                
            }
            if (employee.Roles.FirstOrDefault().RoleName == "Employee")
            {
                var document = _context.Documents.FirstOrDefault(e => e.FileName == viewModel.DocumentName && e.EmployeeId == employee.EmployeeId);
                if (document != null)
                {
                    document.EmployeeId = employee.EmployeeId;
                    _context.SaveChanges();
                    
                }
                else
                {
                    return Unauthorized("Document Employee is not changed");
                }

               
            }
            var documents = _context.Documents.ToList();
            return Ok("File edited");
        }
    }
}
