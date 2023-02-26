namespace SPT.Models
{
    public class DisplayedProducts
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int? SubscriptionPeriod { get; set; }

        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int ClientProductId { get; set; }

    }
}
