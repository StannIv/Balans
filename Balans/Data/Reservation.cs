namespace Balans.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ClientsId { get; set; }
        public Client Clients { get; set; }
        public int ServiceId { get; set; }
        public Service Services { get; set; }
        public DateTime Dateregister { get; set; } = DateTime.Now;
    }
}
