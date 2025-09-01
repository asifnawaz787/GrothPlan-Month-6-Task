using Microsoft.AspNetCore.Identity;

namespace GrothPlan_Month_6_Task.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    


    
        public class ApplicationUser : IdentityUser
        {
            // Add custom fields here if you need them
            // public string FullName { get; set; }
        }
    

}
