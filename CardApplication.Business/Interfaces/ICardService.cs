using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardApplication.Business.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<Models.CardContract>> GetAllCardDetails();
        Task<Models.CardContract> GetACardDetails(long cardNumber);
        Task<Models.CardContract> RegisterNewCard(Models.CardContract card);
    }
}
