using Microsoft.EntityFrameworkCore;

namespace order_getter_csharp {
    public class DatabaseContext : DbContext {
        public DbSet<City> cities { get; set; }
        public DbSet<DeliveryPoint> points { get; set; }
        public DbSet<Order> orders { get; set; }

        public class City {
            public int CityId { get; set; } 
            public string Name { get; set; }
        }
        public class DeliveryPoint {
            public int DeliveryPointId { get; set; }
            public string Name { get; set; }
        }
        public class Order {
            public int OrderId { get; set; }
            public string Name { get; set; }
        }
    }
}
