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

    public List<Transaction> GetTransactions(string userId)
    {
        var transactions = _repositoryWrapper.TransactionRepository.FindAll().Where(x=>x.Sender.User.Id == userId).ToList();
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
        if (sender.Currency == reciver.Currency)
        {
            reciver.Balance = reciver.Balance + ammount;
        }
        else if (sender.Currency == "RON" && reciver.Currency != "RON")
        {

            if (reciver.Currency == "USD")
            {
                reciver.Balance = reciver.Balance + ammount * 3 / 10;
            }
            else
            {
                reciver.Balance = reciver.Balance + ammount * 2 / 10;
            }
        }
        else if (sender.Currency == "USD" && reciver.Currency != "USD")
        {

            if (reciver.Currency == "RON")
            {
                reciver.Balance = reciver.Balance + ammount * 5;
            }
            else
            {
                reciver.Balance = reciver.Balance + ammount * 1;
            }
        }
        else if (sender.Currency == "EURO" && reciver.Currency != "EURO")
        {

            if (reciver.Currency == "RON")
            {
                reciver.Balance = reciver.Balance + ammount * 4;
            }
            else
            {
                reciver.Balance = reciver.Balance + ammount * 1;
            }
        }

        _repositoryWrapper.BankAccountRepository.Update(sender);
        _repositoryWrapper.BankAccountRepository.Update(reciver);
    }

}
