using Library_Management.Connection;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Library_Management.Authorization
{
    public class Admin_Requirement : IAuthorizationRequirement
    {}
}
