namespace Balans.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientsId { get; set; }
        public Client Clients { get; set; }
        public int ItemsId { get; set; }
        public Item Items { get; set; }
        public int Quantity { get; set; }
        public DateTime Dateregister { get; set; }
    }
}
