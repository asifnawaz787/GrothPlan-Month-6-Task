using GrothPlan_Month_6_Task.Data;
using GrothPlan_Month_6_Task.Entities;
using GrothPlan_Month_6_Task.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrothPlan_Month_6_Task.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(LoginViewModel viewModel)
        {
          var employee=  _context.Employees.FirstOrDefault(e => e.Email == viewModel.Email);
            if (employee == null)
            {
                return Unauthorized("invalid user");
            }
            if(employee.Password!=viewModel.Password)
            {
                return Unauthorized("Invalid user name or password");
            }
            return Ok("user authprize");
        }
    }
}
