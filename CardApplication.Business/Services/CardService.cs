using CardApplication.DataAccess.Models;
using CardApplication.DataAccess.Interfaces;
using AutoMapper;
using CardApplication.Business.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace CardApplication.Business.Services
{
    public class CardService : ICardService
    {
        private ICardRepository _cardRepository;
        private IMapper _mapper;

        public CardService(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.CardContract>> GetAllCardDetails()
        {
            return _mapper.Map<IEnumerable<Models.CardContract>>(await _cardRepository.GetAllCardDetails());
        }

        public async Task<Models.CardContract> GetACardDetails(long cardNumber)
        {
            if (cardNumber <= 0) {
                return null;
            }

            return _mapper.Map<Models.CardContract>(await _cardRepository.GetACardDetails(cardNumber));
        }

        public async Task<Models.CardContract> RegisterNewCard(Models.CardContract card) { 
            string[] formats = { "dd/MM/yyyy" };
            CardDbModel cardDbModel = new CardDbModel() { 
                CardGuid = Guid.NewGuid().ToString(),
                CardNumber = long.Parse(card.CardNumber),
                Expiry = DateTime.ParseExact(card.Expiry, formats, new CultureInfo("en-US"), DateTimeStyles.None),
                Name = card.Name
            };
            return _mapper.Map<Models.CardContract>(await _cardRepository.RegisterNewCard(cardDbModel));
        }

    }
}
