using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;
using BankingApplication.Services.Interfaces;
using System.Text;

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
        var bankAccounts = _repositoryWrapper.BankAccountRepository.FindAll().Where(x=>x.isService==false).ToList();
        return bankAccounts;
    }

    public List<BankAccount> GetBankAccountsOfUser(string userId)
    {
        var bankAccounts = _repositoryWrapper.BankAccountRepository.FindAll().Where(x => x.isService == false && x.User.Id == userId).ToList();
        return bankAccounts;
    }

    public List<BankAccount> GetBankAccountsThatAreService()
    {
        var bankAccounts = _repositoryWrapper.BankAccountRepository.FindAll().Where(x => x.isService == true).ToList();
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

    public string GenerateSwift()
    {
        // Define the possible characters for each position in the SWIFT code
        string bankChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string countryChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string locationChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string branchChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Use a StringBuilder to build the SWIFT code
        StringBuilder swiftBuilder = new StringBuilder();

        // Generate the bank code (4 letters)
        for (int i = 0; i < 4; i++)
        {
            swiftBuilder.Append(bankChars[new Random().Next(0, bankChars.Length)]);
        }

        // Generate the country code (2 letters)
        for (int i = 0; i < 2; i++)
        {
            swiftBuilder.Append(countryChars[new Random().Next(0, countryChars.Length)]);
        }

        // Generate the location code (2 letters or digits)
        for (int i = 0; i < 2; i++)
        {
            swiftBuilder.Append(locationChars[new Random().Next(0, locationChars.Length)]);
        }

        // Generate the branch code (3 letters or digits) - optional
        if (new Random().NextDouble() < 0.5)
        {
            for (int i = 0; i < 3; i++)
            {
                swiftBuilder.Append(branchChars[new Random().Next(0, branchChars.Length)]);
            }
        }

        // Return the completed SWIFT code
        return swiftBuilder.ToString();
    }

}
