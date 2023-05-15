using BankingApplication.Models;
using BankingApplication.Repositories.Interfaces;
using BankingApplication.Services;
using BankingApplication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationTest
{
    [TestClass]
    public class CardTests
    {
        private CardService cardService;
        private Mock<IRepositoryWrapper> repositoryWrapper;

        [TestInitialize]
        public void Setup()
        {
            repositoryWrapper = new Mock<IRepositoryWrapper>();

            cardService = new CardService(repositoryWrapper.Object);
        }

        [TestMethod]
        public void GetCards_Returns_ListOfCards()
        {
            IdentityUser user = new IdentityUser("user");

            Card card1 = new Card(1, user);
            Card card2 = new Card(1, user);


            List<Card> Cards = new List<Card> { card1, card2 };

            repositoryWrapper.Setup(r => r.CardRepository.FindAll()).Returns(Cards.AsQueryable());

            var result = cardService.GetCards();

            Assert.IsTrue(result.SequenceEqual(Cards));
        }


        [TestMethod]
        public void FindCard_True()
        {
            IdentityUser user = new IdentityUser("user");

            Card card = new Card(1,  user);

            repositoryWrapper.Setup(r => r.CardRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<Card> { card }.AsQueryable());

            var result = cardService.GetCardById(card.Id);

            Assert.AreEqual(card, result);
        }

        [TestMethod]
        public void FindCard_False()
        {
            IdentityUser user = new IdentityUser("user");

            Card card = new Card(1, user);

            repositoryWrapper.Setup(r => r.CardRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<Card> { }.AsQueryable());

            var result = cardService.GetCardById(card.Id);

            Assert.AreNotEqual(card, result);
        }

        [TestMethod]
        public void Update_Card()
        {
            IdentityUser user = new IdentityUser("user");

            Card card = new Card(1, user);

            repositoryWrapper.Setup(r => r.CardRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<Card> { card }.AsQueryable());

            repositoryWrapper.Setup(r => r.CardRepository.Update(It.IsAny<Card>()))
               .Verifiable();

            var result = cardService.GetCardById(card.Id);

            Assert.AreEqual(card, result);
        }

        [TestMethod]
        public void Delete_Card()
        {
            IdentityUser user = new IdentityUser("user");

            Card card = new Card(1, user);

            repositoryWrapper.Setup(r => r.CardRepository.FindByCondition(x => x.Id == 1))
                .Returns(new List<Card> { }.AsQueryable());

            repositoryWrapper.Setup(r => r.CardRepository.Delete(It.IsAny<Card>()));

            cardService.Delete(card.Id);

            var result = cardService.GetCardById(card.Id);

            Assert.AreEqual(null, result);
        }
    }
}

