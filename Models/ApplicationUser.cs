using Microsoft.AspNetCore.Identity;
namespace appuser;
public class ApplicationUser : IdentityUser {
    public string Name { get; set; } = default!;
}