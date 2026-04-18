using Microsoft.EntityFrameworkCore;

namespace aspire.template.ApiService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {}
}