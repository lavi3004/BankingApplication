using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingApplication.Models;

public class BankingApplicationContext: IdentityDbContext<IdentityUser>
{
    public BankingApplicationContext(DbContextOptions<BankingApplicationContext> options)
        : base(options)
    { }

    public DbSet<Card>? Cards { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }
    public DbSet<BankAccount>? BankAccounts { get; set;}
}
