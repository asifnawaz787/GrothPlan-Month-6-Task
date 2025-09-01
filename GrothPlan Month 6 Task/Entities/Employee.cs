using GrothPlan_Month_6_Task.Data;

namespace GrothPlan_Month_6_Task.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }   // PK
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigation properties
        public ICollection<Document> Document { get; set; } = new List<Document>();
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }

    public class Role
    {
        public int RoleId { get; set; }        // PK
        public string RoleName { get; set; }

        // FK
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

    public class Document
    {
        public int Id { get; set; }            // PK
        public string FileName { get; set; }

        // FK
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}