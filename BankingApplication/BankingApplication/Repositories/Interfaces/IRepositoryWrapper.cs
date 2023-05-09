namespace BankingApplication.Repositories.Interfaces;

public interface IRepositoryWrapper
{
   ICardRepository CardRepository { get; }
   ITransactionRepository TransactionRepository { get; }
   IBankAccountRepository BankAccountRepository { get; }

    void Save();
}
