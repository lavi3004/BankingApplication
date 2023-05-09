using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;

namespace BankingApplication.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankingApplicationContext bankingApplicationContext) : base(bankingApplicationContext)
        {

        }
    }
}
