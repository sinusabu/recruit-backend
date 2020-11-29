using CardApplication.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardApplication.DataAccess.Interfaces
{
    public interface ICardRepository
    {
        Task<IEnumerable<CardDbModel>> GetAllCardDetails();
        Task<CardDbModel> RegisterNewCard(CardDbModel card);
        Task<CardDbModel> GetACardDetails(long cardNumber);
    }
}
