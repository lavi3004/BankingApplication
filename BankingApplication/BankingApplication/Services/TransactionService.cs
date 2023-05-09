using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;
using BankingApplication.Services.Interfaces;

namespace BankingApplication.Services;

public class TransactionService: ITransactionService
{
    private IRepositoryWrapper _repositoryWrapper;

    public TransactionService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public List<Transaction> GetTransactions()
    {
        var transactions = _repositoryWrapper.TransactionRepository.FindAll().ToList();
        return transactions;
    }

    public void Create(Transaction transaction)
    {
        _repositoryWrapper.TransactionRepository.Create(transaction);
    }

    public void Delete(int id)
    {
        var transaction = _repositoryWrapper.TransactionRepository.FindByCondition(x => x.Id == id).FirstOrDefault();
        _repositoryWrapper.TransactionRepository.Delete(transaction);
    }

    public void Update(Transaction transaction)
    {
        _repositoryWrapper.TransactionRepository.Update(transaction);
    }

    public Transaction GetTransactionById(int id)
    {
        return _repositoryWrapper.TransactionRepository.FindByCondition(transaction => transaction.Id == id).FirstOrDefault();
    }


    public void PerformTransaction(int? senderId, int? reciverId, int ammount)
    {
        BankAccount sender = _repositoryWrapper.BankAccountRepository.FindByCondition(x => x.Id == senderId).FirstOrDefault();
        BankAccount reciver = _repositoryWrapper.BankAccountRepository.FindByCondition(x => x.Id == reciverId).FirstOrDefault();

        sender.Balance = sender.Balance - ammount;
        reciver.Balance = sender.Balance + ammount;

        _repositoryWrapper.BankAccountRepository.Update(sender);
        _repositoryWrapper.BankAccountRepository.Update(reciver);
    }

}
