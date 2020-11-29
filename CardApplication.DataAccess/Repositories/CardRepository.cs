using CardApplication.DataAccess.Interfaces;
using CardApplication.DataAccess.Models;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Transactions;

namespace CardApplication.DataAccess.Repositories
{
    public class CardRepository : ICardRepository
    {
        private IDbConnection _connection;

        public CardRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CardDbModel> RegisterNewCard(CardDbModel card)
        {
            var count = 0;
            using (var transactionScope = new TransactionScope())
            {
                string query = @"insert into card (CardNumber, CardGuid, Expiry, Name) values (@CardNumber, @CardGuid, @Expiry, @Name)";
                using (IDbConnection conn = _connection)
                {
                    conn.Open();
                    count = await conn.ExecuteAsync(query, card);
                }
                transactionScope.Complete();
                return count > 0 ? card : null;
            }
        }

        public async Task<CardDbModel> GetACardDetails(long cardNumber)
        {
            CardDbModel card = null;
            string query = @"select * from card where CardNumber=@cardNumber";
            using (IDbConnection conn = _connection)
            {
                conn.Open();
                card = await conn.QueryFirstOrDefaultAsync<CardDbModel>(query, new { cardNumber });
            }
            return card;
        }

        public async Task<IEnumerable<CardDbModel>> GetAllCardDetails()
        {
            IEnumerable<CardDbModel> cards = null;
            string query = @"select * from card";
            using (IDbConnection conn = _connection)
            {
                conn.Open();
                cards = await conn.QueryFirstOrDefaultAsync<IEnumerable<CardDbModel>>(query);
            }
            return cards;
        }
    }
}
