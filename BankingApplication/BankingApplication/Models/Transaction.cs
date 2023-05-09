namespace BankingApplication.Models;

public class Transaction
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public int? SenderId { get; set; }
    public BankAccount? Sender { get; set; }

    public int? ReciverId { get; set; }
    public BankAccount? Reciver { get; set; } 
}
