using BankingApplication.Models;

namespace BankingApplication.Services.Interfaces;

public interface ICardService
{
    public List<Card> GetCards();
    public void Create(Card card);
    public void Update(Card card);
    public void Delete(int id);
    public Card GetCardById(int id);
}
