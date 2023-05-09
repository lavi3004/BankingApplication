using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;
using BankingApplication.Services.Interfaces;

namespace BankingApplication.Services;

public class BankAccountService: IBankAccountService
{
    private IRepositoryWrapper _repositoryWrapper;

    public BankAccountService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public List<BankAccount> GetBankAccounts()
    {
        var bankAccounts = _repositoryWrapper.BankAccountRepository.FindAll().ToList();
        return bankAccounts;
    }

    public void Create(BankAccount bankAccount)
    {
        _repositoryWrapper.BankAccountRepository.Create(bankAccount);
    }

    public void Delete(int id)
    {
        var author = _repositoryWrapper.BankAccountRepository.FindByCondition(x => x.Id == id).FirstOrDefault();
        _repositoryWrapper.BankAccountRepository.Delete(author);
    }

    public void Update(BankAccount bankAccount)
    {
        _repositoryWrapper.BankAccountRepository.Update(bankAccount);
    }

    public BankAccount GetBankAccountById(int id)
    {
        return _repositoryWrapper.BankAccountRepository.FindByCondition(author => author.Id == id).FirstOrDefault();
    }

}
