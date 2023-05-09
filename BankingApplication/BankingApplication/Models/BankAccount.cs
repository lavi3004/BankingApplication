using Microsoft.AspNetCore.Identity;

namespace BankingApplication.Models;

public class BankAccount
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string IBAN { get; set; }

    public string SWIFT { get; set; }

    public float Balance { get; set; }

    public string Currency { get; set; }

    public bool isService { get; set; }

    public IdentityUser User { get; set; }
}
