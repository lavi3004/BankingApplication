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

    public Transaction()
    {
        
    }

    public Transaction(int Id, int Amount, DateTime Date, int SenderId, int ReciverId)
    {
        this.Id = Id;
        this.Amount = Amount;
        this.Date = Date;
        this.SenderId = SenderId;
        this.ReciverId = ReciverId;
    }
}
