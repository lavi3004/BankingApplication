using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;

namespace BankingApplication.Repositories
{
    public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(BankingApplicationContext bankingApplicationContext) : base(bankingApplicationContext)
        {

        }
    }
}
