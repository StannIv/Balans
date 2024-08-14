namespace Balans.Data
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dateregister { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
