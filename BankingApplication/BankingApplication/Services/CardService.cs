using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;
using BankingApplication.Services.Interfaces;

namespace BankingApplication.Services;

public class CardService:ICardService
{
    private IRepositoryWrapper _repositoryWrapper;

    public CardService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public List<Card> GetCards()
    {
        var cards = _repositoryWrapper.CardRepository.FindAll().ToList();
        return cards;
    }

    public void Create(Card Card)
    {
        _repositoryWrapper.CardRepository.Create(Card);
    }

    public void Delete(int id)
    {
        var card = _repositoryWrapper.CardRepository.FindByCondition(x => x.Id == id).FirstOrDefault();
        _repositoryWrapper.CardRepository.Delete(card);
    }

    public void Update(Card card)
    {   
        _repositoryWrapper.CardRepository.Update(card);     
    }

    public Card GetCardById(int id)
    {
        return _repositoryWrapper.CardRepository.FindByCondition(card => card.Id == id).FirstOrDefault();
    }

    public Card GetCardByName(string name)
    {
        return _repositoryWrapper.CardRepository.FindByCondition(Card => Card.Name == name).FirstOrDefault();
    }
}
