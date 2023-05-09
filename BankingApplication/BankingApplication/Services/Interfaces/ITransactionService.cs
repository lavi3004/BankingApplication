using BankingApplication.Models;

namespace BankingApplication.Services.Interfaces;

public interface ITransactionService
{
    public List<Transaction> GetTransactions();
    public void Create(Transaction transaction);
    public void Update(Transaction transaction);
    public void Delete(int id);
    public Transaction GetTransactionById(int id);
    public void PerformTransaction(int? senderId, int? reciverId, int ammount);
}
