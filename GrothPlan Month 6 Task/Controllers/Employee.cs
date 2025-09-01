using GrothPlan_Month_6_Task.Data;
using GrothPlan_Month_6_Task.Entities;
using GrothPlan_Month_6_Task.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrothPlan_Month_6_Task.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public IActionResult AddEmployee([FromForm] AddEmployee viewModel)
        {
            var extension = Path.GetExtension(viewModel.Upload.FileName);
            if (extension == ".pdf" || extension == ".xml")
            {
                var fileName = "wwwroot/Documents/" + viewModel.DocumentName + Path.GetExtension(viewModel.Upload.FileName);
                if (!Directory.Exists("wwwroot/Documents"))
                {
                    Directory.CreateDirectory("wwwroot/Documents");
                }
                using (var stream = new FileStream(fileName, FileMode.Create))
                {

                    viewModel.Upload.CopyTo(stream);
                }
                
            }
            else
            {
                return Ok("only pdf and xml files are allowed to upload");
            }
            
            _context.Employees.Add(new Entities.Employee()
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Password = viewModel.Password,
                Phone = viewModel.Phone,
                Roles = new List<Role>
            {
                new Role
                {
                    
                    
                    RoleName = viewModel.RoleName
                }
            },
                Document=new List<Document>
                {
                    new Document {
                        FileName=viewModel.DocumentName
                    }
                }
               
            });
            _context.SaveChanges();
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        public IActionResult Index()
        {
            return View();
        }

    }

}
