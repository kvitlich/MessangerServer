using System.Data.Entity;

namespace ServerMessanger
{
    public class ServerMessangerDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public ServerMessangerDbContext() : base("Server=KVITLICH;Database=Messagner;Trusted_Connection=True;")
        {
            Database.CreateIfNotExists();
        }
    }
}
