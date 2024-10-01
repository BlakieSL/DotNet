using Microsoft.AspNetCore.Identity;

namespace WebApplication5.Models;

public class ApplicationUser : IdentityUser
{
    public string RefreshToken { get; set; }
}