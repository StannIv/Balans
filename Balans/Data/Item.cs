namespace Balans.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CatalougeNumber { get; set; }
        public string TireWidth { get; set; }
        public string Friction { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturers { get; set; }
        public string Ratio { get; set; }
        public string TireDiameter { get; set; }
        public string SeasonType { get; set; }
        public string VolumeMeter { get; set; }
        public string Photos { get; set; }
        public decimal Price    { get; set; }
        public DateTime Dateregister { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
