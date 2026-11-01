using Library_Management.Connection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library_Management.Authorization
{
    public class AdminAuthorizationHandler : AuthorizationHandler<Admin_Requirement>
    {
        private readonly ApplicationDbContext _context;
        public AdminAuthorizationHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            Admin_Requirement requirement)
        {
            var email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            
            if (email == null)
            {
                return;
            }

            var user = await _context.User_Credentials.FirstOrDefaultAsync(x => x.User_Email == email);

            if (user != null && user.User_Role == "Admin")
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }



        }
    }
}

