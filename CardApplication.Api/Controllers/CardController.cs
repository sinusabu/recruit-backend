using Microsoft.AspNetCore.Mvc;
using CardApplication.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using CardApplication.Business.Models;
using System.Threading.Tasks;

namespace CardApplication.Api.Controllers
{
    [Route("card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetAllCardDetails()
        {
            var response = await _cardService.GetAllCardDetails();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{cardNumber}")]
        public async Task<IActionResult> GetACardDetails([FromRoute][Required] long cardNumber)
        {
            var response = await _cardService.GetACardDetails(cardNumber);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> RegisterNewCard([FromBody][Required] CardContract card)
        {
            var response = await _cardService.RegisterNewCard(card);
            return Ok(response);
        }
    }
}
