﻿namespace Balans.Data
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public DateTime Dateregister { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
