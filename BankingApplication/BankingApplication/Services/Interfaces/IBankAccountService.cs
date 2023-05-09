using BankingApplication.Models;

namespace BankingApplication.Services.Interfaces;

public interface IBankAccountService
{
    public List<BankAccount> GetBankAccounts();
    public void Create(BankAccount bankAccount);
    public void Update(BankAccount bankAccount);
    public void Delete(int id);
    public BankAccount GetBankAccountById(int id);
    public List<BankAccount> GetBankAccountsThatAreService();
    public string GenerateSwift();

    //public BankAccount GetAuthorByName(string name);
}
