using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;

namespace BankingApplication.Repositories
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(BankingApplicationContext bankingApplicationContext):base(bankingApplicationContext)
        {
            
        }
    }
}
