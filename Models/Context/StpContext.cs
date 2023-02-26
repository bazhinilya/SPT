using System.Data.Entity;

namespace SPT.Models.Context
{
    public class StpContext : DbContext
    {
        public StpContext() : base("StpDataBase") { }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ClientsProducts> ClientsProducts { get; set; }
    }
}
