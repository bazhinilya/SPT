namespace SPT.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int? SubscriptionPeriod { get; set; }
    }
}
