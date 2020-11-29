using CardApplication.Api.Controllers;
using CardApplication.Business.Interfaces;
using CardApplication.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CardApplication.Tests
{
    public class CardApplicationControllerTests
    {
        Mock<ICardService> _mockCardService = new Mock<ICardService>();
        CardController _cardController;

        public CardApplicationControllerTests()
        {
            _mockCardService = new Mock<ICardService>();
            _cardController = new CardController(_mockCardService.Object);
        }
 
        [Fact]
        public async Task Test_Card_Controller_For_AllCardDetails()
        {
            //Arrange
            var mockCardService = new Mock<ICardService>();
            var cards = new List<CardContract>(){
                new CardContract()
                {
                    CardNumber = "1000012324231",
                    Name = "Test1",
                    Expiry = "01/01/2023",
                    Cvc = "100"
                },
                new CardContract()
                {
                    CardNumber = "1000012324232",
                    Name = "Test",
                    Expiry = "01/01/2023",
                    Cvc = "200"
                },
                new CardContract()
                {
                    CardNumber = "1000012324233",
                    Name = "Test",
                    Expiry = "01/01/2023",
                    Cvc = "200"
                }
            };
            mockCardService.Setup(f => f.GetAllCardDetails())
                .ReturnsAsync(cards);
            var cardController = new CardController(mockCardService.Object);

            //Act
            var result = await cardController.GetAllCardDetails();
            var okObjectResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(cards, okObjectResult.Value);
        }

        [Fact]
        public async Task Test_Card_Controller_For_Registering_NewCard()
        {
            //Arrange
            var mockCardService = new Mock<ICardService>();
            CardContract card = new CardContract()
            {
                CardNumber = "1000012324232",
                Name = "Test",
                Expiry = "01/01/2023",
                Cvc = "200"
            };

            mockCardService.Setup(f => f.RegisterNewCard(card))
                .ReturnsAsync(card);
            var cardController = new CardController(mockCardService.Object);

            //Act
            var result = await cardController.RegisterNewCard(card);
            var okObjectResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(card, okObjectResult.Value);
        }
    }
}
