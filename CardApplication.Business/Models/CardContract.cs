namespace CardApplication.Business.Models
{
    public class CardContract
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public string Expiry { get; set; }
    }
}
