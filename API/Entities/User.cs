using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class User : IdentityUser<int>
{
    public UserAddress Address { get; set; }
    // Navigation property for linking User to Customer (one-to-one)
    public virtual Customer Customer { get; set; }
}