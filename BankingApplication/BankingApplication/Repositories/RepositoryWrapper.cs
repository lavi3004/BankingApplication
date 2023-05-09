using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;

namespace BankingApplication.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private BankingApplicationContext _bankingApplicatoinContext;
    private ICardRepository? _cardRepository;
    private ITransactionRepository? _transactionRepository;
    private IBankAccountRepository? _bankAccountRepository;

    public ICardRepository CardRepository
    {
        get
        {
            if (_cardRepository == null)
            {
                _cardRepository = new CardRepository(_bankingApplicatoinContext);
            }

            return _cardRepository;
        }
    }


    public ITransactionRepository TransactionRepository
    {
        get
        {
            if (_transactionRepository == null)
            {
                _transactionRepository = new TransactionRepository(_bankingApplicatoinContext);
            }

            return _transactionRepository;
        }
    }


    public IBankAccountRepository BankAccountRepository
    {
        get
        {
            if (_bankAccountRepository == null)
            {
                _bankAccountRepository = new BankAccountRepository(_bankingApplicatoinContext);
            }

            return _bankAccountRepository;
        }
    }

    public RepositoryWrapper(BankingApplicationContext locationContext)
    {
        _bankingApplicatoinContext = locationContext;
    }

    public void Save()
    {
        _bankingApplicatoinContext.SaveChanges();
    }
}

