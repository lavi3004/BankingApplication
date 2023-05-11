using Microsoft.AspNetCore.Identity;

namespace BankingApplication.Models;

public class Card
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string CardNumber { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int CVV { get; set; }

    public IdentityUser User { get; set; }

    public bool IsLocked { get; set; }

    public Card()
    {
        
    }

    public Card(int Id, IdentityUser User)
    {
        this.Id = Id;
        this.User = User;
    }
}


