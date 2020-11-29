using AutoMapper;
using CardApplication.Api.Controllers;
using CardApplication.Business.Interfaces;
using CardApplication.Business.Services;
using CardApplication.DataAccess.Interfaces;
using CardApplication.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Xunit;

namespace CardApplication.Tests
{
    public class CardApplicationIntegrationTests
    {
        string _dbConfigString;
        IDbConnection _dbConnection;
        ICardRepository _cardRepository;
        Mock<IMapper> _mapper = new Mock<IMapper>();
        ICardService _cardService;
        CardController _cardController;
        
        public CardApplicationIntegrationTests()
        {
            _dbConfigString = Environment.GetEnvironmentVariable("CardApplicationConnectionString");
            _dbConnection = new MySqlConnection(_dbConfigString);
            _cardRepository = new CardRepository(_dbConnection);
            _cardService = new CardService(_cardRepository, _mapper.Object);
        }

        [Fact]
        public async Task Test_Card_Controller_For_AllCardDetails()
        {
            _cardController = new CardController(_cardService);

            //Act
            var result = await _cardController.GetAllCardDetails();
            var okObjectResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult.StatusCode);
        }
    }
}
