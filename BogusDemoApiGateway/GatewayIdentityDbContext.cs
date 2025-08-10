using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BogusDemoApiGateway;

internal class GatewayIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public GatewayIdentityDbContext(DbContextOptions<GatewayIdentityDbContext> options)
        : base(options)
    {
    }
}